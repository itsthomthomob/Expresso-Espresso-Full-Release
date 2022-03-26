using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityGrid : MonoBehaviour
{

	// Inspector values
	[SerializeField] private Vector2 Scale;
	[SerializeField] private Vector2 Center;
	[SerializeField] private RectTransform Root;

	// Resources
	private SortedDictionary<EntityPriority, RectTransform> Parents;
	private Dictionary<Vector2Int, List<EntityBase>> Grid;
	private Dictionary<EntityBase, MovingEntity> Moving;

	private void Reset()
	{
		Scale = Vector2.one;
		Center = Vector2.zero;
		Root = GetComponent<RectTransform>();
	}

	public EntityGrid()
	{
		Parents = new SortedDictionary<EntityPriority, RectTransform>();
		Grid = new Dictionary<Vector2Int, List<EntityBase>>();
		Moving = new Dictionary<EntityBase, MovingEntity>();
	}

	private struct MovingEntity
	{
		public Vector2Int From;
		public Vector2Int To;
		public float Duration;
		public float Start;
	}

	private void Update()
	{
		float time = Time.time;
		foreach (KeyValuePair<EntityBase, MovingEntity> pair in Moving)
		{
			if (time < pair.Value.Start + pair.Value.Duration)
			{
				float factor = (time - pair.Value.Start) / pair.Value.Duration;
				Vector2 position = Vector2.LerpUnclamped(pair.Value.From, pair.Value.To, factor);
				SetEntityRect(pair.Key, position);
			}
			else if (pair.Key.IsMoving)
			{
				pair.Key.OnGridMoveEnd();
			}
		}
	}

	public List<EntityBase> GetAllEntities()
	{
		List<EntityBase> list = new List<EntityBase>();
		foreach (List<EntityBase> entities in Grid.Values)
		{
			list.AddRange(entities);
		}
		return list;
	}

	public T Create<T>(Vector2Int position) where T : EntityBase
	{

		// Create entity
		string name = typeof(T).Name;
		name = name.StartsWith("Entity") ? name.Remove(0, 6) : name;
		GameObject obj = new GameObject(name, typeof(RectTransform));
		Image image = obj.AddComponent<Image>();
		T entity = obj.AddComponent<T>();

		// Notify entity
		entity.OnGridCreate(this, position);
		entity.OnEntityAwake();

		// Set entity priority
		SetEntityPriority(entity, entity.Priority);

		// Set entity sprite
		SetEntitySprite(entity, entity.Sprite);

		// Set entity transform
		SetEntityRect(entity, position);

		// Add to grid
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) Grid[position] = list = new List<EntityBase>();
		list.Add(entity);
		list.Sort(CompareEntities);

		// Return entity
		return entity;

	}

	public void Destroy(EntityBase entity)
	{

		// Validate
		if (entity == null || entity.Grid == null) return;
		if (entity.Grid != this) throw new InvalidOperationException("Entity is not on this grid");

		// Notify and destroy the entity
		entity.OnGridDestroy();
		Destroy(entity.gameObject);

		// Remove from grid
		Grid.TryGetValue(entity.Position, out List<EntityBase> list);
		if (list != null) list.Remove(entity);
		Moving.Remove(entity);

	}

	public void DestroyAll(Vector2Int position)
	{

		// Get the list of entities at the position
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null || list.Count == 0) return;

		// Destroy entities
		foreach (EntityBase entity in list)
		{
			Moving.Remove(entity);
			entity.OnGridDestroy();
			Destroy(entity.gameObject);
		}

		// Clear the list
		list.Clear();

	}

	public void Move(EntityBase entity, Vector2Int position, float seconds = 0f)
	{

		// Validate
		if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity is null");
		if (entity.Grid == null) throw new InvalidOperationException("Entity has no grid");
		if (entity.Grid != this) throw new InvalidOperationException("Entity is not on this grid");
		if (entity.Position == position) return;

		// Remove from previous position
		Vector2Int from = entity.Position;
		Grid.TryGetValue(from, out List<EntityBase> previous);
		if (previous != null) previous.Remove(entity);

		// Add to new position
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) Grid[position] = list = new List<EntityBase>();
		list.Add(entity);
		list.Sort(CompareEntities);

		if (seconds > 0f)
		{

			// Mark entity as moving
			Moving[entity] = new MovingEntity()
			{
				From = from,
				To = position,
				Duration = seconds,
				Start = Time.time,
			};

			// Update and notify
			SetEntityRect(entity, from);
			entity.OnGridMoveBegin(position);

		}
		else
		{

			// Update and notify
			Moving.Remove(entity);
			SetEntityRect(entity, position);
			entity.OnGridTeleport(position);

		}

		// Reorder siblings
		Transform parent = entity.RectTransform.parent;
		int sibling = GetSortedSiblingIndex(parent, entity);
		entity.RectTransform.SetSiblingIndex(sibling);

	}

	public void UpdateEntityPriority(EntityBase entity)
	{
		SetEntityPriority(entity, entity.Priority);
	}

	public void UpdateEntitySprite(EntityBase entity)
	{
		SetEntitySprite(entity, entity.Sprite);
		SetEntityRect(entity, entity.Position);
	}

	private void SetEntityRect(EntityBase entity, Vector2 position)
	{
		Rect rect = entity.Sprite.rect;
		Vector4 border = entity.Sprite.border;
		Vector2 size = new Vector2(rect.size.x - border.x - border.z, rect.size.y - border.y - border.w);
		Vector2 min = new Vector2((position.x - 0.5f - Center.x - border.x / size.x) * Scale.x, (position.y - 0.5f - Center.y - border.y / size.y) * Scale.y);
		Vector2 max = new Vector2((position.x + 0.5f - Center.x + border.z / size.x) * Scale.x, (position.y + 0.5f - Center.y + border.w / size.y) * Scale.y);
		entity.RectTransform.anchorMin = min;
		entity.RectTransform.anchorMax = max;
		entity.RectTransform.pivot = new Vector2(0.5f, 0.5f);
		entity.RectTransform.offsetMin = Vector2.zero;
		entity.RectTransform.offsetMax = Vector2.zero;
	}

	private void SetEntitySprite(EntityBase entity, Sprite sprite)
	{
		entity.Image.sprite = sprite;
	}

	private void SetEntityPriority(EntityBase entity, EntityPriority priority)
	{

		// Get priority parent or create a new one if it doesn't exist yet
		Parents.TryGetValue(priority, out RectTransform parent);
		if (parent == null)
		{
			int index = 0;
			GameObject p = new GameObject(priority.ToString(), typeof(RectTransform));
			parent = p.GetComponent<RectTransform>();
			parent.SetParent(Root, false);
			parent.anchorMin = Vector2.zero;
			parent.anchorMax = Vector2.one;
			parent.pivot = new Vector2(0.5f, 0.5f);
			parent.offsetMin = Vector2.zero;
			parent.offsetMax = Vector2.zero;
			Parents[priority] = parent;
			foreach (KeyValuePair<EntityPriority, RectTransform> pair in Parents)
			{
				pair.Value.SetSiblingIndex(index);
				index++;
			}
		}

		// Set entity parent
		int sibling = GetSortedSiblingIndex(parent, entity);
		entity.RectTransform.SetParent(parent, false);
		entity.RectTransform.SetSiblingIndex(sibling);

	}

	public int GetEntityCount(Vector2Int position)
	{
		Grid.TryGetValue(position, out List<EntityBase> list);
		return list == null ? 0 : list.Count;
	}

	public bool HasEntity<T>(Vector2Int position) where T : EntityBase
	{
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return false;
		foreach (EntityBase entity in list)
		{
			if (entity is T) return true;
		}
		return false;
	}

	public bool HasPriority(Vector2Int position, EntityPriority priority)
	{
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return false;
		foreach (EntityBase entity in list)
		{
			if (entity.Priority == priority) return true;
		}
		return false;
	}

	public EntityBase[] GetEntities(Vector2Int position)
	{
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return new EntityBase[0];
		return list.ToArray();
	}

	public T[] GetEntities<T>(Vector2Int position) where T : EntityBase
	{
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return new T[0];
		List<T> found = new List<T>();
		foreach (EntityBase entity in list)
		{
			if (entity is T t) found.Add(t);
		}
		return found.ToArray();
	}

	public T GetFirstEntity<T>(Vector2Int position) where T : EntityBase
	{
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return null;
		foreach (EntityBase entity in list)
		{
			if (entity is T t) return t;
		}
		return null;
	}

	public T GetLastEntity<T>(Vector2Int position) where T : EntityBase
	{
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return null;
		T last = null;
		foreach (EntityBase entity in list)
		{
			if (entity is T t) last = t;
		}
		return last;
	}

	public EntityBase[] FindEntities(Vector2Int a, Vector2Int b, bool inclusive = false)
	{
		Vector2Int min = Vector2Int.Min(a, b);
		Vector2Int max = Vector2Int.Max(a, b);
		if (inclusive) max += Vector2Int.one;
		List<EntityBase> list = new List<EntityBase>();
		for (int x = min.x; x < max.x; x++)
		{
			for (int y = min.y; y < max.y; y++)
			{
				Vector2Int position = new Vector2Int(x, y);
				Grid.TryGetValue(position, out List<EntityBase> found);
				if (found != null) list.AddRange(found);
			}
		}
		return list.ToArray();
	}

	public T[] FindEntities<T>(Vector2Int a, Vector2Int b, bool inclusive = false) where T : EntityBase
	{
		Vector2Int min = Vector2Int.Min(a, b);
		Vector2Int max = Vector2Int.Max(a, b);
		if (inclusive) max += Vector2Int.one;
		List<T> list = new List<T>();
		for (int x = min.x; x < max.x; x++)
		{
			for (int y = min.y; y < max.y; y++)
			{
				Vector2Int position = new Vector2Int(x, y);
				Grid.TryGetValue(position, out List<EntityBase> found);
				if (found == null) continue;
				foreach (EntityBase entity in found)
				{
					if (entity is T t) found.Add(t);
				}
			}
		}
		return list.ToArray();
	}

	public void FloodFill(Vector2Int source, Func<Queue<Vector2Int>, Vector2Int, bool> visit)
	{
		Vector2Int[] sources = new Vector2Int[] { source };
		Queue<Vector2Int> queue = new Queue<Vector2Int>(sources);
		HashSet<Vector2Int> nodes = new HashSet<Vector2Int>(sources);
		while (queue.Count > 0 && nodes.Count < Grid.Count)
		{
			Vector2Int current = queue.Dequeue();
			if (visit.Invoke(queue, current))
			{
				Vector2Int a = new Vector2Int(current.x + 1, current.y + 0);
				Vector2Int b = new Vector2Int(current.x + 0, current.y + 1);
				Vector2Int c = new Vector2Int(current.x - 1, current.y + 0);
				Vector2Int d = new Vector2Int(current.x + 0, current.y - 1);
				if (GetEntityCount(a) > 0 && nodes.Add(a)) queue.Enqueue(a);
				if (GetEntityCount(b) > 0 && nodes.Add(b)) queue.Enqueue(b);
				if (GetEntityCount(c) > 0 && nodes.Add(c)) queue.Enqueue(c);
				if (GetEntityCount(d) > 0 && nodes.Add(d)) queue.Enqueue(d);
			}
		}
	}

	public T FindNearestEntity<T>(Vector2Int source) where T : EntityBase
	{
		T entity = null;
		FloodFill(source, (queue, position) => {
			entity = GetFirstEntity<T>(position);
			if (entity == null)
			{
				return true;
			}
			else
			{
				queue.Clear();
				return false;
			}
		});
		return entity;
	}

	private struct PathfindNode
	{
		public Vector2Int Position;
		public Vector2Int Next;
	}

	public bool Pathfind(Vector2Int origin, Vector2Int destination, Predicate<Vector2Int> passable, out Vector2Int next)
	{

		// Validate
		if (passable == null) throw new ArgumentNullException(nameof(passable), "No passable predicate");

		// Find all passable destinations
		HashSet<Vector2Int> destinations = new HashSet<Vector2Int>();
		FloodFill(destination, (queue, position) => {
			destinations.Add(position);
			if (passable.Invoke(position))
			{
				return false;
			}
			else
			{
				return true;
			}
		});

		// Get all return values
		Vector2Int a = new Vector2Int(origin.x + 1, origin.y + 0);
		Vector2Int b = new Vector2Int(origin.x + 0, origin.y + 1);
		Vector2Int c = new Vector2Int(origin.x - 1, origin.y + 0);
		Vector2Int d = new Vector2Int(origin.x + 0, origin.y - 1);

		// If destination is one of the return values, return it
		if (destinations.Contains(a)) { next = GetEntityCount(a) > 0 && passable.Invoke(a) ? a : origin; return true; }
		if (destinations.Contains(b)) { next = GetEntityCount(b) > 0 && passable.Invoke(b) ? b : origin; return true; }
		if (destinations.Contains(c)) { next = GetEntityCount(c) > 0 && passable.Invoke(c) ? c : origin; return true; }
		if (destinations.Contains(d)) { next = GetEntityCount(d) > 0 && passable.Invoke(d) ? d : origin; return true; }

		// Initialize flood fill
		Queue<PathfindNode> queue = new Queue<PathfindNode>();
		HashSet<Vector2Int> visited = new HashSet<Vector2Int>(new Vector2Int[] { origin });
		if (GetEntityCount(a) > 0 && passable.Invoke(a) && visited.Add(a)) queue.Enqueue(new PathfindNode() { Position = a, Next = a });
		if (GetEntityCount(b) > 0 && passable.Invoke(b) && visited.Add(b)) queue.Enqueue(new PathfindNode() { Position = b, Next = b });
		if (GetEntityCount(c) > 0 && passable.Invoke(c) && visited.Add(c)) queue.Enqueue(new PathfindNode() { Position = c, Next = c });
		if (GetEntityCount(d) > 0 && passable.Invoke(d) && visited.Add(d)) queue.Enqueue(new PathfindNode() { Position = d, Next = d });

		// Flood fill
		while (queue.Count > 0 && visited.Count < Grid.Count)
		{
			PathfindNode current = queue.Dequeue();
			a = new Vector2Int(current.Position.x + 1, current.Position.y + 0);
			b = new Vector2Int(current.Position.x + 0, current.Position.y + 1);
			c = new Vector2Int(current.Position.x - 1, current.Position.y + 0);
			d = new Vector2Int(current.Position.x + 0, current.Position.y - 1);
			if (destinations.Contains(a)) { next = current.Next; return true; }
			if (destinations.Contains(b)) { next = current.Next; return true; }
			if (destinations.Contains(c)) { next = current.Next; return true; }
			if (destinations.Contains(d)) { next = current.Next; return true; }
			if (GetEntityCount(a) > 0 && passable.Invoke(a) && visited.Add(a)) queue.Enqueue(new PathfindNode() { Position = a, Next = current.Next });
			if (GetEntityCount(b) > 0 && passable.Invoke(b) && visited.Add(b)) queue.Enqueue(new PathfindNode() { Position = b, Next = current.Next });
			if (GetEntityCount(c) > 0 && passable.Invoke(c) && visited.Add(c)) queue.Enqueue(new PathfindNode() { Position = c, Next = current.Next });
			if (GetEntityCount(d) > 0 && passable.Invoke(d) && visited.Add(d)) queue.Enqueue(new PathfindNode() { Position = d, Next = current.Next });
		}

		// No path found
		next = origin;
		return false;

	}

	private static int GetSortedSiblingIndex(Transform parent, EntityBase entity)
	{
		int sibling = 0;
		foreach (Transform child in parent)
		{
			int compared = CompareEntities(child.GetComponent<EntityBase>(), entity);
			if (compared >= 0 && child != entity) return sibling;
			sibling++;
		}
		return sibling;
	}

	private static int CompareEntities(EntityBase a, EntityBase b)
	{
		if (a == b) return 0;
		if (a == null || b == null) return -1;
		int compared = a.Priority.CompareTo(b.Priority);
		if (compared != 0) return compared;
		return b.Position.y.CompareTo(a.Position.y);
	}

}

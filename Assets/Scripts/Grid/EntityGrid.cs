using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityGrid : MonoBehaviour {

	// Inspector values
	[SerializeField] private Vector2 Scale;
	[SerializeField] private Vector2 Center;
	[SerializeField] private RectTransform Root;

	// Resources
	private SortedDictionary<EntityPriority, RectTransform> Parents;
	private Dictionary<Vector2Int, List<EntityBase>> Grid;
	private Dictionary<EntityBase, MovingEntity> Moving;

	private void Reset() {
		Scale = Vector2.one;
		Center = Vector2.zero;
		Root = GetComponent<RectTransform>();
	}

	public EntityGrid() {
		Parents = new SortedDictionary<EntityPriority, RectTransform>();
		Grid = new Dictionary<Vector2Int, List<EntityBase>>();
		Moving = new Dictionary<EntityBase, MovingEntity>();
	}

	private struct MovingEntity {
		public Vector2Int From;
		public Vector2Int To;
		public float Duration;
		public float Start;
	}

	private void Update() {
		float time = Time.time;
		foreach (KeyValuePair<EntityBase, MovingEntity> pair in Moving) {
			if (time < pair.Value.Start + pair.Value.Duration) {
				float factor = (time - pair.Value.Start) / pair.Value.Duration;
				Vector2 position = Vector2.LerpUnclamped(pair.Value.From, pair.Value.To, factor);
				UpdateTransform(pair.Key, position);
			} else if (pair.Key.IsMoving) {
				pair.Key.OnGridMoveEnd();
			}
		}
	}

	public T Create<T>(Vector2Int position) where T : EntityBase {

		// Create entity
		string name = typeof(T).Name;
		name = name.StartsWith("Entity") ? name.Remove(0, 6) : name;
		GameObject obj = new GameObject(name, typeof(RectTransform));
		Image image = obj.AddComponent<Image>();
		T entity = obj.AddComponent<T>();

		// Notify entity
		entity.OnGridCreate(this, position);

		// Get priority parent or create a new one if it doesn't exist yet
		EntityPriority priority = entity.Priority;
		Parents.TryGetValue(priority, out RectTransform parent);
		if (parent == null) {
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
			foreach (KeyValuePair<EntityPriority, RectTransform> pair in Parents) {
				pair.Value.SetSiblingIndex(index);
				index++;
			}
		}

		// Set entity parent
		int sibling = GetSortedSiblingIndex(parent, entity);
		entity.RectTransform.SetParent(parent, false);
		entity.RectTransform.SetSiblingIndex(sibling);

		// Set entity sprite
		Sprite sprite = entity.Sprite;
		entity.Image.sprite = sprite;

		// Set entity transform
		UpdateTransform(entity, position);

		// Add to grid and return
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) Grid[position] = list = new List<EntityBase>();
		list.Add(entity);
		list.Sort(CompareEntities);
		return entity;

	}

	public void Destroy(EntityBase entity) {

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

	public void DestroyAll(Vector2Int position) {

		// Get the list of entities at the position
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null || list.Count == 0) return;

		// Destroy entities
		foreach (EntityBase entity in list) {
			Moving.Remove(entity);
			entity.OnGridDestroy();
			Destroy(entity.gameObject);
		}

		// Clear the list
		list.Clear();

	}

	public void Move(EntityBase entity, Vector2Int position, float seconds = 0f) {

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

		if (seconds > 0f) {

			// Mark entity as moving
			Moving[entity] = new MovingEntity() {
				From = from,
				To = position,
				Duration = seconds,
				Start = Time.time,
			};

			// Update and notify
			UpdateTransform(entity, from);
			entity.OnGridMoveBegin(position);

		} else {

			// Update and notify
			Moving.Remove(entity);
			UpdateTransform(entity, position);
			entity.OnGridTeleport(position);

		}

		// Reorder siblings
		Transform parent = entity.RectTransform.parent;
		int sibling = GetSortedSiblingIndex(parent, entity);
		entity.RectTransform.SetSiblingIndex(sibling);

	}

	private struct PathfindNode {
		public Vector2Int Position;
		public Vector2Int Next;
	}

	public Vector2Int Pathfind(Vector2Int origin, Vector2Int destination, Predicate<Vector2Int> passable) {

		// Validate
		if (passable == null) throw new ArgumentNullException(nameof(passable), "No passable predicate");

		// Initialize
		Queue<PathfindNode> queue = new Queue<PathfindNode>();
		HashSet<Vector2Int> visited = new HashSet<Vector2Int>(new Vector2Int[] { origin });
		HashSet<Vector2Int> destinations = new HashSet<Vector2Int>(new Vector2Int[] { destination });
		Vector2Int a = new Vector2Int(origin.x + 1, origin.y + 0);
		Vector2Int b = new Vector2Int(origin.x + 0, origin.y + 1);
		Vector2Int c = new Vector2Int(origin.x - 1, origin.y + 0);
		Vector2Int d = new Vector2Int(origin.x + 0, origin.y - 1);
		if (!passable.Invoke(destination)) FloodFillInverted(destinations, passable);
		if (destinations.Contains(a)) return GetEntityCount(a) > 0 && passable.Invoke(a) ? a : origin;
		if (destinations.Contains(b)) return GetEntityCount(b) > 0 && passable.Invoke(b) ? b : origin;
		if (destinations.Contains(c)) return GetEntityCount(c) > 0 && passable.Invoke(c) ? c : origin;
		if (destinations.Contains(d)) return GetEntityCount(d) > 0 && passable.Invoke(d) ? d : origin;
		if (GetEntityCount(a) > 0 && passable.Invoke(a) && visited.Add(a)) queue.Enqueue(new PathfindNode() { Position = a, Next = a });
		if (GetEntityCount(b) > 0 && passable.Invoke(b) && visited.Add(b)) queue.Enqueue(new PathfindNode() { Position = b, Next = b });
		if (GetEntityCount(c) > 0 && passable.Invoke(c) && visited.Add(c)) queue.Enqueue(new PathfindNode() { Position = c, Next = c });
		if (GetEntityCount(d) > 0 && passable.Invoke(d) && visited.Add(d)) queue.Enqueue(new PathfindNode() { Position = d, Next = d });
		
		// Flood fill
		while (queue.Count > 0 && visited.Count < Grid.Count) {
			PathfindNode current = queue.Dequeue();
			a = new Vector2Int(current.Position.x + 1, current.Position.y + 0);
			b = new Vector2Int(current.Position.x + 0, current.Position.y + 1);
			c = new Vector2Int(current.Position.x - 1, current.Position.y + 0);
			d = new Vector2Int(current.Position.x + 0, current.Position.y - 1);
			if (destinations.Contains(a)) return current.Next;
			if (destinations.Contains(b)) return current.Next;
			if (destinations.Contains(c)) return current.Next;
			if (destinations.Contains(d)) return current.Next;
			if (GetEntityCount(a) > 0 && passable.Invoke(a) && visited.Add(a)) queue.Enqueue(new PathfindNode() { Position = a, Next = current.Next });
			if (GetEntityCount(b) > 0 && passable.Invoke(b) && visited.Add(b)) queue.Enqueue(new PathfindNode() { Position = b, Next = current.Next });
			if (GetEntityCount(c) > 0 && passable.Invoke(c) && visited.Add(c)) queue.Enqueue(new PathfindNode() { Position = c, Next = current.Next });
			if (GetEntityCount(d) > 0 && passable.Invoke(d) && visited.Add(d)) queue.Enqueue(new PathfindNode() { Position = d, Next = current.Next });
		}

		// No path found
		return origin;

	}

	public void FloodFill(HashSet<Vector2Int> nodes, Predicate<Vector2Int> passable) {
		Queue<Vector2Int> queue = new Queue<Vector2Int>(nodes);
		while (queue.Count > 0 && nodes.Count < Grid.Count) {
			Vector2Int current = queue.Dequeue();
			Vector2Int a = new Vector2Int(current.x + 1, current.y + 0);
			Vector2Int b = new Vector2Int(current.x + 0, current.y + 1);
			Vector2Int c = new Vector2Int(current.x - 1, current.y + 0);
			Vector2Int d = new Vector2Int(current.x + 0, current.y - 1);
			if (GetEntityCount(a) > 0 && passable.Invoke(a) && nodes.Add(a)) queue.Enqueue(a);
			if (GetEntityCount(b) > 0 && passable.Invoke(b) && nodes.Add(b)) queue.Enqueue(b);
			if (GetEntityCount(c) > 0 && passable.Invoke(c) && nodes.Add(c)) queue.Enqueue(c);
			if (GetEntityCount(d) > 0 && passable.Invoke(d) && nodes.Add(d)) queue.Enqueue(d);
		}
	}

	public void FloodFillInverted(HashSet<Vector2Int> nodes, Predicate<Vector2Int> passable) {
		Queue<Vector2Int> queue = new Queue<Vector2Int>(nodes);
		while (queue.Count > 0 && nodes.Count < Grid.Count) {
			Vector2Int current = queue.Dequeue();
			Vector2Int a = new Vector2Int(current.x + 1, current.y + 0);
			Vector2Int b = new Vector2Int(current.x + 0, current.y + 1);
			Vector2Int c = new Vector2Int(current.x - 1, current.y + 0);
			Vector2Int d = new Vector2Int(current.x + 0, current.y - 1);
			if (GetEntityCount(a) > 0 && !passable.Invoke(a) && nodes.Add(a)) queue.Enqueue(a);
			if (GetEntityCount(b) > 0 && !passable.Invoke(b) && nodes.Add(b)) queue.Enqueue(b);
			if (GetEntityCount(c) > 0 && !passable.Invoke(c) && nodes.Add(c)) queue.Enqueue(c);
			if (GetEntityCount(d) > 0 && !passable.Invoke(d) && nodes.Add(d)) queue.Enqueue(d);
		}
	}

	public int GetEntityCount(Vector2Int position) {
		Grid.TryGetValue(position, out List<EntityBase> list);
		return list == null ? 0 : list.Count;
	}

	public bool HasEntity<T>(Vector2Int position) where T : EntityBase {
		Grid.TryGetValue(position, out List<EntityBase> list);
		foreach (EntityBase entity in list) if (entity is T) return true;
		return false;
	}

	public EntityBase[] GetEntities(Vector2Int position) {
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return new EntityBase[0];
		return list.ToArray();
	}

	public T[] GetEntities<T>(Vector2Int position) where T : EntityBase {
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return new T[0];
		List<T> found = new List<T>();
		foreach (EntityBase entity in list) {
			if (entity is T t) found.Add(t);
		}
		return found.ToArray();
	}

	public T GetFirstEntity<T>(Vector2Int position) where T : EntityBase {
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return null;
		foreach (EntityBase entity in list) {
			if (entity is T t) return t;
		}
		return null;
	}

	public T GetLastEntity<T>(Vector2Int position) where T : EntityBase {
		Grid.TryGetValue(position, out List<EntityBase> list);
		if (list == null) return null;
		T last = null;
		foreach (EntityBase entity in list) {
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

	private void UpdateTransform(EntityBase entity, Vector2 position) {
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

	private static int GetSortedSiblingIndex(Transform parent, EntityBase entity) {
		int sibling = 0;
		foreach (Transform child in parent) {
			int compared = CompareEntities(child.GetComponent<EntityBase>(), entity);
			if (compared >= 0 && child != entity) return sibling;
			sibling++;
		}
		return sibling;
	}

	private static int CompareEntities(EntityBase a, EntityBase b) {
		if (a == b) return 0;
		if (a == null || b == null) return -1;
		int compared = a.Priority.CompareTo(b.Priority);
		if (compared != 0) return compared;
		return b.Position.y.CompareTo(a.Position.y);
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
	[SerializeField] private RectTransform Parent;
	[SerializeField] private Vector2Int Size;
	private List<Entity>[][] GridList;

	private void Awake()
	{
		GridList = new List<Entity>[Size.x][];
		for (int x = 0; x < Size.x; x++)
		{
			GridList[x] = new List<Entity>[Size.y];
			for (int y = 0; y < Size.y; y++)
			{
				GridList[x][y] = new List<Entity>();
			}
		}
	}

	public Vector2Int GetRoadRowsMin() 
	{ 
		return new Vector2Int(0, 0);
	}
	public Vector2Int GetRoadRowsMax()
	{
		return new Vector2Int(Size.x, 2);
	}

	public Vector2Int GetGridMinimum()
	{
		return new Vector2Int(0, 0);
	}

	public Vector2Int GetGridMaximum()
	{
		return Size;
	}

	public RectTransform GetParent()
	{
		return Parent;
	}

	public void Clear(Vector2Int position)
	{
		if (position.x < 0 || position.x >= Size.x) return;
		if (position.y < 0 || position.y >= Size.y) return;
		foreach (Entity entity in GridList[position.x][position.y]) entity.OnGridRemove();
		GridList[position.x][position.y].Clear();
	}

	public T Create<T>(Vector2Int position) where T : Entity
	{
		GameObject obj = new GameObject(typeof(T).Name, typeof(RectTransform));
		obj.AddComponent<RawImage>();
		T entity = obj.AddComponent<T>();
		Add(entity, position);
		return entity;
	}

	public void Add(Entity entity, Vector2Int position)
	{
		if (position.x < 0 || position.x >= Size.x) throw new Exception("Invalid position");
		if (position.y < 0 || position.y >= Size.y) throw new Exception("Invalid position");
		if (entity == null) throw new Exception("Invalid entity");
		Grid grid = entity.GetGrid();
		if (grid == this && position == entity.GetGridPosition()) return;
		if (grid != null) grid.Remove(entity);
		List<Entity> list = GridList[position.x][position.y];
		list.Add(entity);
		list.Sort(ComparePriority);
		Vector2 min = new Vector2((position.x + 0) / (float)Size.x, (position.y + 0) / (float)Size.y);
		Vector2 max = new Vector2((position.x + 1) / (float)Size.x, (position.y + 1) / (float)Size.y);
		entity.OnGridAdd(this, position, min, max);
		for (int i = 0; i < list.Count; i++) list[i].transform.SetSiblingIndex(i);
	}

	public void Remove(Entity entity)
	{
		if (entity == null) return;
		Grid grid = entity.GetGrid();
		if (grid == null) return;
		if (grid != this) throw new Exception("Invalid grid");
		Vector2Int position = entity.GetGridPosition();
		if (position.x < 0 || position.x >= Size.x) throw new Exception("Invalid position");
		if (position.y < 0 || position.y >= Size.y) throw new Exception("Invalid position");
		GridList[position.x][position.y].Remove(entity);
		entity.OnGridRemove();
	}

	private static int ComparePriority(Entity a, Entity b)
	{
		if (a == b) return 0;
		if (a == null || b == null) return 1;
		return a.GetZPriority().CompareTo(b.GetZPriority());
	}

	public void Move(Entity entity, Vector2Int target, float duration)
	{
		// TODO
	}

	public Entity[] GetEntities(Vector2Int position)
	{
		// TODO
		return null;
	}

	public Entity[] FindEntities(Vector2Int min, Vector2Int max)
	{
		// TODO
		return null;
	}

	public Entity[] FindEntities(Vector2Int center, float radius)
	{
		// TODO
		return null;
	}

	public T[] GetEntities<T>(Vector2Int position) where T : Entity
	{
		// TODO
		return null;
	}

	public T[] FindEntities<T>(Vector2Int min, Vector2Int max) where T : Entity
	{
		// TODO
		return null;
	}

	public T[] FindEntities<T>(Vector2Int center, float radius) where T : Entity
	{
		// TODO
		return null;
	}
}

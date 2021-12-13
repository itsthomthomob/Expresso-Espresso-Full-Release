using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AIType
{
    CustomerAI,
    EmployeeAI,
    Nonpassable
}

public enum TileZone
{
    Customers,
    Employees,
    Storage
}

public enum EntityType 
{
    Customers,
    Employees,
    Storage,
    CustomerAI,
    EmployeeAI,
    Nonpassable
}

[RequireComponent(typeof(RectTransform), typeof(RawImage))]
public abstract class Entity : MonoBehaviour
{
	private Grid Grid = null;
	private Vector2Int Position = Vector2Int.zero;
	private RectTransform RectTransform = null;
	private Texture2D Texture = null;
	private RawImage Image = null;

	public abstract int GetZPriority();
	public abstract Texture2D GetEntityTexture();

	public Grid GetGrid()
	{
		return Grid;
	}

	public Vector2Int GetGridPosition()
	{
		return Position;
	}

	internal void OnGridAdd(Grid grid, Vector2Int position, Vector2 min, Vector2 max)
	{
		Grid = grid;
		Position = position;
		gameObject.SetActive(true);
		Image = Image != null ? Image : GetComponent<RawImage>();
		Texture = Texture != null ? Texture : GetEntityTexture();
		RectTransform = RectTransform != null ? RectTransform : GetComponent<RectTransform>();
		RectTransform.SetParent(grid.GetParent(), false);
		RectTransform.anchorMin = min;
		RectTransform.anchorMax = max;
		RectTransform.pivot = new Vector2(0.5f, 0.5f);
		RectTransform.offsetMin = Vector2.zero;
		RectTransform.offsetMax = Vector2.zero;
		Image.texture = Texture;
	}

	internal void OnGridRemove()
	{
		Grid = null;
		Position = new Vector2Int(0, 0);
		gameObject.SetActive(false);
	}
	public abstract void OnGridMoveBegin(Grid grid, Vector2Int position, float duration);
    public abstract void OnGridMoveEnd(Grid grid, Vector2Int position, float duration);

    public abstract void SetTileZone(TileZone newZone);
    public abstract TileZone GetTileZone();

    public abstract string GetEntityName();
    public abstract int GetEntityCost();
    public abstract int GetEntitySpeed();
    public abstract bool IsEntityPassable();
    public abstract AIType GetAIType();
}
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public abstract class EntityBase : MonoBehaviour
{

	private EntityGrid CacheGrid = null;
	private Vector2Int CachePosition = Vector2Int.zero;
	private EntityPriority CachePriority = EntityPriority.Unknown;
	private RectTransform CacheRect = null;
	private Image CacheImage = null;
	private Sprite CacheSprite = null;
	private string CacheName = "";

	private bool MovingCache = false;

	public virtual string OnSerialize() { return null; }
	public virtual void OnDeserialize(string state) { }

	public EntityGrid Grid
	{
		get => CacheGrid;
	}

	public Vector2Int Position
	{
		get => CachePosition;
	}

	public EntityPriority Priority
	{
		get => CachePriority;
	}

	public RectTransform RectTransform
	{
		get => CacheRect ??= GetComponent<RectTransform>();
	}

	public Image Image
	{
		get => CacheImage ??= GetComponent<Image>();
	}

	public Sprite Sprite
	{
		get => CacheSprite;
	}

	public string Name
	{
		get => CacheName;
	}

	public bool IsMoving
	{
		get => MovingCache;
	}

	public abstract void OnEntityAwake();

	public void Move(Vector2Int position, float seconds = 0f)
	{
		if (CacheGrid == null)
		{
			throw new InvalidOperationException("No grid");
		}
		else
		{
			CacheGrid.Move(this, position, seconds);
		}
	}

	protected void SetEntityPriority(EntityPriority priority)
	{
		CachePriority = priority;
		Grid.UpdateEntityPriority(this);
	}

	protected void SetEntitySprite(Sprite sprite)
	{
		CacheSprite = sprite;
		Grid.UpdateEntitySprite(this);
	}

	protected void SetEntityName(string name)
	{
		CacheName = name;
	}

	internal void OnGridCreate(EntityGrid grid, Vector2Int position)
	{
		CacheGrid = grid;
		CachePosition = position;
	}

	internal void OnGridMoveBegin(Vector2Int position)
	{
		CachePosition = position;
		MovingCache = true;
	}

	internal void OnGridMoveEnd()
	{
		MovingCache = false;
	}

	internal void OnGridTeleport(Vector2Int position)
	{
		CachePosition = position;
		MovingCache = false;
	}

	internal void OnGridDestroy()
	{
		CacheGrid = null;
	}

}

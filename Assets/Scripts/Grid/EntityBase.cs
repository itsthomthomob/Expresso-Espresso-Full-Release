using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public abstract class EntityBase : MonoBehaviour{

	private EntityGrid GridCache = null;
	private Vector2Int PositionCache = Vector2Int.zero;
	private RectTransform RectTransformCache = null;
	private Sprite SpriteCache = null;
	private Image ImageCache = null;
	private bool MovingCache = false;

	public Vector2Int Position => PositionCache;
	public EntityGrid Grid => GridCache;
	public RectTransform RectTransform => RectTransformCache ??= GetComponent<RectTransform>();
	public Image Image  => ImageCache ??= GetComponent<Image>();
	public Sprite Sprite => SpriteCache ??= GetEntitySprite();
	public EntityPriority Priority => GetEntityPriority();
	public bool IsMoving => MovingCache;

	protected abstract EntityPriority GetEntityPriority();
	protected abstract Sprite GetEntitySprite();

	public void Move(Vector2Int position, float seconds = 0f) {
		if (GridCache == null) {
			throw new InvalidOperationException("No grid");
		} else {
			GridCache.Move(this, position, seconds);
		}
	}

	internal void OnGridCreate(EntityGrid grid, Vector2Int position) {
		GridCache = grid;
		PositionCache = position;
	}

	internal void OnGridMoveBegin(Vector2Int position) {
		PositionCache = position;
		MovingCache = true;
	}

	internal void OnGridMoveEnd() {
		MovingCache = false;
	}

	internal void OnGridTeleport(Vector2Int position) {
		PositionCache = position;
		MovingCache = false;
	}

	internal void OnGridDestroy() {
		GridCache = null;
	}
}

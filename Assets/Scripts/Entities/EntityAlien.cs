using UnityEngine;

public class EntityAlien : EntityBase {

	private World World;
	private Vector2Int Destination;
	private float Speed;

	private void Awake() {
		World = FindObjectOfType<World>();
		Destination = World.GetRandomLocation();
		Speed = Random.Range(0.1f, 0.5f);
	}

	protected override Sprite GetEntitySprite() {
		return Resources.Load<Sprite>("Sprites/Alien");
	}

	protected override EntityPriority GetEntityPriority() {
		return EntityPriority.Characters;
	}

	private void Update() {
		if (!IsMoving) {
			if (Position == Destination) Destination = World.GetRandomLocation();
			Vector2Int next = Grid.Pathfind(Position, Destination, IsPassableForAlien);
			if (next == Position) Destination = World.GetRandomLocation();
			Grid.Move(this, next, Speed);
		}
	}

	private bool IsPassableForAlien(Vector2Int position) {
		if (Grid.HasEntity<EntityWall>(position)) {
			return false;
		} else {
			return true;
		}
	}

}

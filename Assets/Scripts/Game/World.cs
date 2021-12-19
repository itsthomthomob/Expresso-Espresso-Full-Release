using UnityEngine;

public class World : MonoBehaviour {

	private EntityGrid Grid;
	private Vector2Int Min;
	private Vector2Int Max;

	private void Start() {

		// Find grid
		Grid = FindObjectOfType<EntityGrid>();

		// Grid size
		Min = new Vector2Int(-20, -10);
		Max = new Vector2Int(+20, +10);
		int size = (Max.x - Min.x) * (Max.y - Min.y);

		// Concrete
		for (int x = Min.x; x <= Max.x; x++) {
			for (int y = Min.y; y <= Max.y; y++) {
				Grid.Create<EntityConcrete>(new Vector2Int(x, y));
			}
		}

		// Walls
		for (int i = 0; i < size / 4; i++) {
			Vector2Int position = GetRandomLocation();
			Grid.DestroyAll(position);
			Grid.Create<EntityWall>(position);
		}

		// Road
		for (int x = Min.x; x < Max.x; x++) {
			Vector2Int position = new Vector2Int(x, 0);
			Grid.DestroyAll(position);
			Grid.Create<EntityRoad>(position);
		}

		// Aliens
		for (int i = 0; i < 100; i++) {
			Grid.Create<EntityAlien>(GetRandomLocation());
		}

	}

	public Vector2Int GetRandomLocation() {
		return new Vector2Int(Random.Range(Min.x, Max.x), Random.Range(Min.y, Max.y));
	}

}

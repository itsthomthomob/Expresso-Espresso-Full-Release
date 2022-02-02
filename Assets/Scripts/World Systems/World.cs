using UnityEngine;

public class World : MonoBehaviour {

	private EntityGrid Grid;
	private Vector2Int Min;
	private Vector2Int Max;

	private void Start() {

		// Find grid
		Grid = FindObjectOfType<EntityGrid>();

		// Grid size
		Min = new Vector2Int(-20, -20);
		Max = new Vector2Int(+20, +20);
		int size = (Max.x - Min.x) * (Max.y - Min.y);

		// Grass
		for (int x = Min.x; x <= Max.x; x++) {
			for (int y = Min.y; y <= Max.y; y++) {
				Grid.Create<EntityGrass>(new Vector2Int(x, y));
			}
		}
	}

	public Vector2Int GetRandomLocation() {
		return new Vector2Int(Random.Range(Min.x, Max.x), Random.Range(Min.y, Max.y));
	}
}

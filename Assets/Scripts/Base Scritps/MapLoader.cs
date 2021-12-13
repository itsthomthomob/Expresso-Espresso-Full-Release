using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
	private Grid Grid;

	private void Start()
	{
		Grid = FindObjectOfType<Grid>();
		Vector2Int min = Grid.GetGridMinimum();
		Vector2Int max = Grid.GetGridMaximum();
		for (int x = min.x; x < max.x; x++)
		{
			for (int y = min.y; y < max.y; y++)
			{
				Grid.Create<EntityConcrete>(new Vector2Int(x, y));
			}
		}

		LoadRoad();
	}

	private void LoadRoad() 
	{
		Vector2Int min = Grid.GetRoadRowsMin();
		Vector2Int max = Grid.GetRoadRowsMax();

		for (int x = min.x; x < max.x; x++)
		{
			for (int y = min.y; y < max.y; y++)
			{
				Grid.Clear(new Vector2Int(x, y));
				Grid.Create<EntityRoad>(new Vector2Int(x, y));
			}
		}
	}
}

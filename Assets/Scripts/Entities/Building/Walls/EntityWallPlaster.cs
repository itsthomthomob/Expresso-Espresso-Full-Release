using UnityEngine;
using UnityEngine.UI;

public class EntityWallPlaster : EntityBase {

	public void ManageSprites() 
	{ 
		
        if (Grid.GetLastEntity<EntityBase>(new Vector2Int(Position.x, Position.y + 1)) is EntityWallPlaster
			&&
			Grid.GetLastEntity<EntityBase>(new Vector2Int(Position.x, Position.y - 1)) is EntityWallPlaster
			)
        {
			SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/wall1 extension"));
        }
	}

	public void Update()
	{
		if (GetComponent<Image>().sprite != Resources.Load<Sprite>("Sprites/Tiles/Building/wall extension") as Sprite)
		{
			ManageSprites();
		}
	}

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall1"));
		SetEntityPriority(EntityPriority.Buildings);
		SetEntityName("Plaster Wall");
	}
}

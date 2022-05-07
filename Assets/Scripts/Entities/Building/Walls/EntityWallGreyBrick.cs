using UnityEngine;
using UnityEngine.UI;

public class EntityWallGreyBrick : EntityBase {

	public void ManageSprites()
	{
		
		if (Grid.GetLastEntity<EntityBase>(new Vector2Int(Position.x, Position.y + 1)) is EntityWallGreyBrick
			&&
			Grid.GetLastEntity<EntityBase>(new Vector2Int(Position.x, Position.y - 1)) is EntityWallGreyBrick
			)
		{
			SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/wall3 extension"));
		}
	}

	public void Update()
	{
		if (GetComponent<Image>().sprite != Resources.Load<Sprite>("Sprites/Tiles/Building/wall3 extension") as Sprite)
		{
			ManageSprites();
		}
	}

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall3"));
		SetEntityPriority(EntityPriority.Buildings);
		SetEntityName("Grey Brick Wall");
	}
}

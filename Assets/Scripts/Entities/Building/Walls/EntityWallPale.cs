using UnityEngine;
using UnityEngine.UI;

public class EntityWallPale : EntityBase {

	public void ManageSprites()
	{
		
		if (Grid.GetLastEntity<EntityBase>(new Vector2Int(Position.x, Position.y + 1)) is EntityWallPale
			&&
			Grid.GetLastEntity<EntityBase>(new Vector2Int(Position.x, Position.y - 1)) is EntityWallPale
			)
		{
			SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/wall2 extension"));
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
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall2"));
		SetEntityPriority(EntityPriority.Buildings);
		SetEntityName("Pale Wall");
	}
}

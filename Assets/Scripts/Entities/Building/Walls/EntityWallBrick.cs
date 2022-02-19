using UnityEngine;

public class EntityWallBrick : EntityBase {

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall"));
		SetEntityPriority(EntityPriority.Buildings);
		SetEntityName("Brick Wall");
	}
}

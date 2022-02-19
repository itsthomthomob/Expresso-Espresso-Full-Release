using UnityEngine;

public class EntityWallGreyBrick : EntityBase {

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall3"));
		SetEntityPriority(EntityPriority.Buildings);
		SetEntityName("Grey Brick Wall");
	}
}

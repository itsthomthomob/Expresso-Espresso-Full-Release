using UnityEngine;

public class EntityWallPale : EntityBase {

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall2"));
		SetEntityPriority(EntityPriority.Buildings);
		SetEntityName("Pale Wall");
	}
}

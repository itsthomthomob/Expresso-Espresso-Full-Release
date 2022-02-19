using UnityEngine;

public class EntityWallPlaster : EntityBase {

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall1"));
		SetEntityPriority(EntityPriority.Buildings);
		SetEntityName("Plaster Wall");
	}
}

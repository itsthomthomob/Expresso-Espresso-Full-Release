using UnityEngine;

public class EntityWallBrick : EntityBase {

	protected override Sprite GetEntitySprite() {
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Wall");
	}

	protected override EntityPriority GetEntityPriority() {
		return EntityPriority.Buildings;
	}

    public override string GetEntityName()
    {
		return "Brick Wall";
    }
}

using UnityEngine;

public class EntityWallGreyBrick : EntityBase {

	protected override Sprite GetEntitySprite() {
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Wall3");
	}

	protected override EntityPriority GetEntityPriority() {
		return EntityPriority.Buildings;
	}

    public override string GetEntityName()
    {
		return "Grey Brick Wall";
    }
}

using UnityEngine;

public class EntityWallPale : EntityBase {

	protected override Sprite GetEntitySprite() {
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Wall2");
	}

	protected override EntityPriority GetEntityPriority() {
		return EntityPriority.Buildings;
	}

    public override string GetEntityName()
    {
		return "Pale Wall";
    }
}

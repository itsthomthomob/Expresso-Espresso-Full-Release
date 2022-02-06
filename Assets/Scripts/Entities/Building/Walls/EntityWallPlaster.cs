using UnityEngine;

public class EntityWallPlaster : EntityBase {

	protected override Sprite GetEntitySprite() {
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Wall1");
	}

	protected override EntityPriority GetEntityPriority() {
		return EntityPriority.Buildings;
	}

    public override string GetEntityName()
    {
		return "Plaster Wall";
    }
}

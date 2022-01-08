using UnityEngine;

public class EntityWall : EntityBase {

	protected override Sprite GetEntitySprite() {
		return Resources.Load<Sprite>("Sprites/Wall");
	}

	protected override EntityPriority GetEntityPriority() {
		return EntityPriority.Buildings;
	}

}

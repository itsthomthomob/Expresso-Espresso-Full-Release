using UnityEngine;

public class EntityConcrete : EntityBase {

    protected override Sprite GetEntitySprite() {
        return Resources.Load<Sprite>("Sprites/Concrete");
    }

    protected override EntityPriority GetEntityPriority() {
        return EntityPriority.Terrain;
    }

}

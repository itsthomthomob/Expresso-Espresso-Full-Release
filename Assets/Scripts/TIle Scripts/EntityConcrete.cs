using UnityEngine;

public class EntityConcrete : EntityBase {

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Concrete"));
        SetEntityPriority(EntityPriority.Terrain);
        SetEntityName("Concrete");
    }
}

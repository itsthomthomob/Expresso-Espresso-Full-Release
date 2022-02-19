using UnityEngine;

public class EntityRoad : EntityBase {

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Road"));
        SetEntityPriority(EntityPriority.Terrain);
        SetEntityName("Road");
    }
}

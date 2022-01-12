using UnityEngine;

public class EntityRoad : EntityBase {

	protected override Sprite GetEntitySprite() {
        return Resources.Load<Sprite>("Sprites/Road");
    }

    protected override EntityPriority GetEntityPriority() {
        return EntityPriority.Terrain;
    }

    public override string GetEntityName()
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using UnityEngine;


public class GhostEntity : EntityBase
{
    public void SetGhostPriority(EntityPriority newP) 
    {
        SetEntityPriority(newP);
    }

    public void SetGhostSprite(Sprite newSprite) 
    { 
        SetEntitySprite(newSprite);
    }

    public void SetGhostPos(Vector2Int newPos) 
    {
        Move(newPos, 0f);
    }

    public override void OnEntityAwake()
    {
        SetEntityPriority(EntityPriority.Floating);
    }
}

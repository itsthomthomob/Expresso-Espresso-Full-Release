using System.Collections;
using UnityEngine;


public class GhostEntity : EntityBase
{
    public void SetGhostSprite(Sprite newSprite)
    {
        SetEntitySprite(newSprite);
    }

    private void Start()
    {
        //gameObject.SetActive(false);
    }
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor"));
        SetEntityPriority(EntityPriority.Floating);
        SetEntityName("Rough Pale Wooden Floor");
    }
}

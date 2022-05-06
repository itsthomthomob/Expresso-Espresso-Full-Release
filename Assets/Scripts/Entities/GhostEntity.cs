using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GhostEntity : EntityBase
{
    public void SetGhostSprite(Sprite newSprite)
    {
        gameObject.GetComponent<Image>().sprite = newSprite;
    }

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Transparent") as Sprite);
        SetEntityPriority(EntityPriority.Floating);
        SetEntityName("GhostEntity");
    }
}
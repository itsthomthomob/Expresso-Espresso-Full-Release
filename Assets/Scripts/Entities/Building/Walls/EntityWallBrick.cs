using UnityEngine;
using UnityEngine.UI;

public class EntityWallBrick : EntityBase {

    public void ManageSprites()
	{
		if (Grid.GetLastEntity<EntityBase>(new Vector2Int(Position.x, Position.y + 1)) is EntityWallBrick
			&&
			Grid.GetLastEntity<EntityBase>(new Vector2Int(Position.x, Position.y - 1)) is EntityWallBrick
			)
		{
			SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/wall extension"));
		}
	}

    public void Update()
    {
        if (GetComponent<Image>().sprite != Resources.Load<Sprite>("Sprites/Tiles/Building/wall extension") as Sprite)
        {
			ManageSprites();
		}
    }

    public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall"));
		SetEntityPriority(EntityPriority.Buildings);
		SetEntityName("Brick Wall");
	}
}

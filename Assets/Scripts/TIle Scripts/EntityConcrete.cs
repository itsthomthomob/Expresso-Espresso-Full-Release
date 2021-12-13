using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(RawImage))]
class EntityConcrete : Entity
{
    TileZone _MyZone;

    public override AIType GetAIType()
    {
        return AIType.EmployeeAI;
    }

    public override int GetEntityCost()
    {
        return 0;
    }

    public override string GetEntityName()
    {
        return "EntityConcrete";
    }

    public override int GetEntitySpeed()
    {
        return 6;
    }

    public override Texture2D GetEntityTexture()
    {
        return Resources.Load<Texture2D>("Sprites/Tiles/Tile_Concrete");
    }

    public override TileZone GetTileZone()
    {
        return _MyZone;
    }

    public override int GetZPriority()
    {
        return 1;
    }

    public override bool IsEntityPassable()
    {
        return true;
    }

    public override void OnGridMoveBegin(Grid grid, Vector2Int position, float duration)
    {
        throw new System.NotImplementedException();
    }

    public override void OnGridMoveEnd(Grid grid, Vector2Int position, float duration)
    {
        throw new System.NotImplementedException();
    }

    public override void SetTileZone(TileZone newZone)
    {
        _MyZone = newZone;
    }
}

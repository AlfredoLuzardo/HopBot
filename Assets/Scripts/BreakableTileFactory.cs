using UnityEngine;

public class BreakableTileFactory : TileFactory
{
    public Tile CreateTile()
    {
        return new BreakableTile();
    }
}

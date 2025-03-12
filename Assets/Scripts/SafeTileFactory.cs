using UnityEngine;

public class SafeTileFactory : TileFactory
{
    public Tile CreateTile()
    {
        return new SafeTile();
    }
}

using UnityEngine;

public class DangerousTileFactory : TileFactory
{
    public Tile CreateTile()
    {
        return new DangerousTile();
    }
}

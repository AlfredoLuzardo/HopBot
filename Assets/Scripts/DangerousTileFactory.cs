using UnityEngine;

public class DangerousTileFactory : TileFactory
{
    /// <summary>
    /// Returns a new Dangerous Tile
    /// </summary>
    /// <returns></returns>
    public Tile CreateTile()
    {
        return new DangerousTile();
    }
}

using UnityEngine;

public class BreakableTileFactory : TileFactory
{
    /// <summary>
    /// Returns a new Breakable Tile
    /// </summary>
    /// <returns></returns>
    public Tile CreateTile()
    {
        return new BreakableTile();
    }
}

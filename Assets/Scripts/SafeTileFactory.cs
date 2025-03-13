using UnityEngine;

public class SafeTileFactory : TileFactory
{
    /// <summary>
    /// Returns a SafeTiles
    /// </summary>
    /// <returns>new Safetile()</returns>
    public Tile CreateTile()
    {
        return new SafeTile();
    }
}

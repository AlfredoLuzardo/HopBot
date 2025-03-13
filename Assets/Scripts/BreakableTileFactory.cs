using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a factory for Breakable Tile
/// </summary>
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

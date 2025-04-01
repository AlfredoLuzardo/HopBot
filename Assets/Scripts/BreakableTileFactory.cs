using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a factory for Breakable Tile
/// version 1.1
/// </summary>
public class BreakableTileFactory : TileFactory
{
    /// <summary>
    /// Returns a new Breakable Tile
    /// </summary>
    /// <returns></returns>
    public Tile CreateTile()
    {
        GameObject newTileObject;
        BreakableTile breakableTile; 

        newTileObject = new GameObject("BreakableTile");
        breakableTile = newTileObject.AddComponent<BreakableTile>();

        return breakableTile;
    }
}

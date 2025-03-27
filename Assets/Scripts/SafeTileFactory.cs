using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a factory for SafeTile
/// version 1.1
/// </summary>
public class SafeTileFactory : TileFactory
{
    /// <summary>
    /// Returns a SafeTile
    /// </summary>
    /// <returns>new Safetile()</returns>
    public Tile CreateTile()
    {
        GameObject newTileObject;
        SafeTile safeTile;

        newTileObject = new GameObject("SafeTile");
        safeTile = newTileObject.AddComponent<SafeTile>();

        return safeTile;
    }
}

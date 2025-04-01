using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a factory for SafeTile
/// </summary>
public class SafeTileFactory : TileFactory
{
    /// <summary>
    /// Returns a SafeTile
    /// </summary>
    /// <returns>new Safetile()</returns>
    public Tile CreateTile()
    {
        return new SafeTile();
    }
}

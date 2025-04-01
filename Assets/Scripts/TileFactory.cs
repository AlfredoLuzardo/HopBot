using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a TileFactory
/// </summary>
public interface TileFactory
{
    /// <summary>
    /// Create Tile function should return a Tile
    /// </summary>
    /// <returns></returns>
    Tile CreateTile();
}

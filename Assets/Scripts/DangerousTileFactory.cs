using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a factory for dangerous tile
/// </summary>
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

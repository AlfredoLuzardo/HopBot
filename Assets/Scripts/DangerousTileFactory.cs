using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a factory for dangerous tile
/// version 1.1
/// </summary>
public class DangerousTileFactory : TileFactory
{
    /// <summary>
    /// Returns a new Dangerous Tile
    /// </summary>
    /// <returns></returns>
    public Tile CreateTile()
    {
        GameObject newTileObject;
        DangerousTile dangerousTile;

        newTileObject = new GameObject("DangerousTile");
        dangerousTile = newTileObject.AddComponent<DangerousTile>();

        return dangerousTile;
    }
}

using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo
/// Represents a Breakable Tile
/// </summary>
public class BreakableTile : Tile
{
    GameObject breakableTile;
    int durability;
    bool isBroken;

    /// <summary>
    /// Initializes the variables.
    /// </summary>
    /// <param name="durability"></param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    public void Initialize(int durability, 
                           int posX, 
                           int posY)
    {
        base.Initialize(posX, posY);
        this.durability = durability;
    }

    /// <summary>
    /// Sets the gameobject to be attached to script
    /// </summary>
    /// <param name="tileObject"></param>
    public void setTileObject(GameObject tileObject)
    {
        breakableTile = tileObject;
    }

    /// <summary>
    /// Getter for is broken
    /// </summary>
    /// <returns></returns>
    public bool GetIsBroken()
    {
        return isBroken;
    }

    /// <summary>
    /// Destroy function.
    /// Sets isBroken to true.
    /// </summary>
    public void Break()
    {
        isBroken = true;
        if(breakableTile != null)
        {
            Debug.Log("DESTROYED");
            Destroy(breakableTile);
        }
    }

    /// <summary>
    /// Decrements the durability of the tile.
    /// If the durability reaches zero, destroy the tile.
    /// </summary>
    public void DecrementDurability()
    {
        durability--;
        Debug.Log("HIT");
        Debug.Log("DURABILITY: " + durability);

        if (durability == 0)
        {
            Break();
        }
    }
}

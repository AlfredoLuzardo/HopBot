using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo
/// Represents a Breakable Tile
/// </summary>
public class BreakableTile : Tile
{
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
    /// Getter for is broken
    /// </summary>
    /// <returns></returns>
    public bool Get_IsBroken()
    {
        return isBroken;
    }

    /// <summary>
    /// Destroy function.
    /// Sets isBroken to true.
    /// </summary>
    public void Destroy()
    {
        isBroken = true;
    }

    /// <summary>
    /// Decrements the durability of the tile.
    /// If the durability reaches zero, destroy the tile.
    /// </summary>
    public void DecrementDurability()
    {
        durability--;

        if (durability == 0)
        {
            Destroy();
        }
    }
}

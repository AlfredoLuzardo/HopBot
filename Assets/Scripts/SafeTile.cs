using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a SafeTile
/// version 1.1
/// </summary>
public class SafeTile : Tile
{
    private bool isStartTile;
    private bool isEndTile;
    
    /// <summary>
    /// Initializes the variables
    /// </summary>
    /// <param name="startBoolean"></param>
    /// <param name="endBoolean"></param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    public void Initialize(bool startBoolean, 
                           bool endBoolean, 
                           int posX,
                           int posY)
    {
        ValidateSafeTile(startBoolean, endBoolean);
        base.Initialize(posX, posY);
        isStartTile = startBoolean;
        isEndTile   = endBoolean;
    }

    /// <summary>
    /// Getter for isStartTile
    /// </summary>
    /// <returns></returns>
    public bool GetIsStart() => isStartTile;

    /// <summary>
    /// Getter for isEndTile
    /// </summary>
    /// <returns></returns>
    public bool GetIsEnd() => isEndTile;

    /// <summary>
    /// Validation method for SafeTile
    /// </summary>
    /// <param name="startBoolean"></param>
    /// <param name="endBoolean"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void ValidateSafeTile(bool startBoolean, bool endBoolean)
    {
        if (startBoolean && endBoolean)
        {
            Debug.Log("A SafeTile cannot be both a Start tile and an End tile");
        }
    }

    /// <summary>
    /// Generates an item on the safetile.
    /// </summary>
    public void GenerateItem()
    {
        if (!isStartTile && !isEndTile)
        {
            Debug.Log("Generated an Item!!!");
        }
    }

    public void EndRound()
    {
        if(isEndTile)
        {
            Debug.Log("HIT END TILE");
            // switch scenes
        }
    }
}

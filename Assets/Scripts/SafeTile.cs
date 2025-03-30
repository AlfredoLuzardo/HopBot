using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a SafeTile
/// version 1.2
/// </summary>
public class SafeTile : Tile
{
    GameObject item;
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
    /// <returns>isStartTile</returns>
    public bool GetIsStart() => isStartTile;

    /// <summary>
    /// Getter for isEndTile
    /// </summary>
    /// <returns>isEndTile</returns>
    public bool GetIsEnd() => isEndTile;

    /// <summary>
    /// Getter for item 
    /// </summary>
    /// <returns>item</returns>
    public GameObject GetItem() => item;

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
    public void SpawnItem(GameObject itemPrefab)
    {
        if (!isStartTile && !isEndTile)
        {
            item = Instantiate(itemPrefab, 
                               new Vector3(
                                   GetPositionX(), 
                                   0.5f, 
                                   GetPositionY()), 
                               Quaternion.identity);
        }
    }

    /// <summary>
    /// Ends the round if the endtile is hit
    /// </summary>
    public void EndRound()
    {
        if(isEndTile)
        {
            Debug.Log("HIT END TILE");
            // switch scenes
        }
    }
}

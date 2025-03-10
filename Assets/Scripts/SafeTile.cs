using UnityEngine;

public class SafeTile : Tile
{
    private bool isStartTile;
    private bool isEndTile;

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
    public void Generate_Item()
    {
        if (!isStartTile && !isEndTile)
        {
            Debug.Log("Generated an Item!!!");
        }
    }
}

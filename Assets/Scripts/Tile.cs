using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a Tile
/// version 1.0
/// </summary>
public abstract class Tile : MonoBehaviour
{
    private int positionX;
    private int positionY;

    /// <summary>
    /// Initializes Tile
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    public void Initialize(int posX, int posY)
    {
        positionX = posX;
        positionY = posY;
    }

    /// <summary>
    /// Getter for PositionX
    /// </summary>
    /// <returns></returns>
    public int GetPositionX()
    {
        return positionX;
    }

    /// <summary>
    /// Getter for PositionY
    /// </summary>
    /// <returns></returns>
    public int GetPositionY()
    {
        return positionY;
    }
}

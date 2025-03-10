using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // abstract void Start();

    // Update is called once per frame
    // public abstract void Update();

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

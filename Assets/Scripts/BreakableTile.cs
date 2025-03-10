using UnityEngine;

public class BreakableTile : Tile
{
    int durability;
    bool isBroken;

    public void Initialize(int durability, 
                           int posX, 
                           int posY)
    {
        base.Initialize(posX, posY);
        this.durability = durability;
    }

    // /// <summary>
    // /// Constructor for BreakableTile
    // /// </summary>
    // /// <param name="durability"></param>
    // /// <param name="posX"></param>
    // /// <param name="posY"></param>
    // public BreakableTile(int durability, int posX, int posY) : base(posX, posY)
    // {
    //     this.durability = durability;
    //     this.isBroken = false;
    // }

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}

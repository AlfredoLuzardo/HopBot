using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents an item
/// version 1.0
/// </summary>
public abstract class Item : MonoBehaviour
{
    /// <summary>
    /// Detects when the player collides with the item
    /// </summary>
    /// <param name="other"></param>
    public abstract void OnTriggerEnter(Collider other);
}

using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the invincibility item
/// version 1.0
/// </summary>
public class Invincibility : Item
{
    /// <summary>
    /// Detects when the player collides with the invincibility item.
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Invincibility on");
            // Implement invincibility logic here.
            Destroy(this.gameObject);
        }
    }
}

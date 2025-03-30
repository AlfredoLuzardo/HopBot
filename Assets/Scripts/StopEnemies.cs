using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the StopEnemies item
/// version 1.0
/// </summary>
public class StopEnemies : Item
{
    /// <summary>
    /// Detects when the player collides with the StopEnemies item.
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("StopEnemies on");
            // Implement the stopEnemies logic here.
            Destroy(this.gameObject);
        }
    }
}

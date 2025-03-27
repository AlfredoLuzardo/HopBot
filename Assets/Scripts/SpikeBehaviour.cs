using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the behaviour of a spike
/// version 1.1
/// </summary>
public class SpikeBehaviour : MonoBehaviour
{

    /// <summary>
    /// Detects when the player collides with the spike.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT");
            // Implement player decrement health logic here
        }
    }
}

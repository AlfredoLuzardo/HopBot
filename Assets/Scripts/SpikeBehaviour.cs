using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the behaviour of a spike
/// version 1.2
/// </summary>
public class SpikeBehaviour : MonoBehaviour, Harmful
{
    public int damage = 2;

    /// <summary>
    /// Detects when the player collides with the spike.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            GameObject player = collision.gameObject;
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController == null)
            {
                Debug.LogWarning("PlayerController component not found on Player.");
                return;
            }

            Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;

            attack(player);
        }
    }

    /// <summary>
    /// Attacks the player by dealing damage
    /// </summary>
    /// <param name="">object</param>
    public void attack(GameObject player)
    {
        PlayerHealth health;

        health = player.GetComponent<PlayerHealth>();
        Debug.Log("PLAYER HEALTH BEFORE: " + health.GetHealth());

        if(health != null)
        {
            health.TakeDamage(damage, transform.position);
            Debug.Log("PLAYER HEALTH AFTER: " + health.GetHealth());
        }
        else
        {
            Debug.LogWarning("Player does not have PlayerHealth component!");
        }
    }
}

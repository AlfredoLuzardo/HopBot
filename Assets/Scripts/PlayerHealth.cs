using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a player
/// version 1.0
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    private static int health;
    private bool isInvincible;

    /// <summary>
    /// Getter for health
    /// </summary>
    public int GetHealth() => health;

    /// <summary>
    /// Getter for isInvincible
    /// </summary>
    /// <returns></returns>
    public bool GetIsInvincible() => isInvincible;

    /// <summary>
    /// Setter for isInvincible to be true
    /// </summary>
    public void SetInvincible() => isInvincible = true;

    /// <summary>
    /// Setter for isInvincible to be false
    /// </summary>
    public void SetVincible() => isInvincible = false;

    /// <summary>
    /// Start method
    /// </summary>
    public void Start()
    {
        health = 5;
        isInvincible = false;
    }

    /// <summary>
    /// Validation for health.
    /// </summary>
    private static void ValidateHealth()
    {
        if(health <= 0)
        {
            Debug.Log("GAME OVER");
            // Switch scenes
        }
    }

    /// <summary>
    /// TakeDamage method 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            health = health - damage;
            ValidateHealth();
        }
    }
}

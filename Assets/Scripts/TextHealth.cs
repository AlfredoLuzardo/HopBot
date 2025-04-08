using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the health text
/// version 1.0
/// </summary>
public class HealthText : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int health;
    [SerializeField] Text healthText;

    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        PlayerHealth playerHealth;
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        health = playerHealth.GetHealth();
        if (healthText != null && GameManager.Instance != null)
        {
            health = GameManager.Instance.currentHealth;
        }
    }

    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        if (healthText != null && GameManager.Instance != null)
        {
            PlayerHealth playerHealth;
            int currentHealth;

            playerHealth = FindFirstObjectByType<PlayerHealth>();
            currentHealth = playerHealth.GetHealth();

            if (currentHealth <= 0)
            {
                healthText.text = "Health: 0";
            }
            else
            {
                healthText.text = "Health: " + currentHealth;
            }
        }
    }
}

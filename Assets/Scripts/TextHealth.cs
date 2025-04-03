using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the health text
/// version 1.0
/// </summary>
public class HealthText : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] Text healthText;

    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {   
        float currentHealth;
        
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

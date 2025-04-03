using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a player
/// version 1.0
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    private static int health;
    private bool isInvincible;
    private Rigidbody rb;
    private PlayerController playerController;
    private FlashBehaviour flashBehaviour;

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
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        flashBehaviour = GetComponent<FlashBehaviour>();
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
            FindFirstObjectByType<LoseMenu>().GameLost();
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
            BecomeInvincible(2f);
            PushBackToLastTile();
        }
    }

    public void BecomeInvincible(float timeDurationSec)
    {
        SetInvincible();
        StartCoroutine(ResumeVincibility(timeDurationSec));
    }

    public void BecomeInvincible(float timeDurationSec, Color flashColor)
    {
        SetInvincible();
        StartCoroutine(ResumeVincibility(timeDurationSec, flashColor));
    }
    
    /// <summary>
    /// Waits for a num of seconds, then resumes vincibility.
    /// </summary>
    /// <param name="health"></param>
    /// <returns></returns>
    private IEnumerator ResumeVincibility(float timeDurationSec)
    {
        yield return new WaitForSeconds(timeDurationSec);
        SetVincible();
        Debug.Log("NOT INVINCIBLE");
    }

        /// <summary>
    /// Waits for a num of seconds, then resumes vincibility.
    /// </summary>
    /// <param name="health"></param>
    /// <returns></returns>
    private IEnumerator ResumeVincibility(float timeDurationSec, Color flashColor)
    {
        yield return flashBehaviour.FlashCouroutine(timeDurationSec, flashColor, 0.8f);
        SetVincible();
        Debug.Log("NOT INVINCIBLE");
    }

    /// <summary>
    /// Push the player back to the last tile stepped position.
    /// </summary>
    private void PushBackToLastTile()
    {
        if (playerController == null || rb == null) return;

        // Calculate horizontal direction (XZ plane only, ignoring Y-axis)
        Vector3 horizontalDirection = (playerController.lastTileStepped - transform.position);
        horizontalDirection.y = 0; // Remove vertical component

        float pushForce = 1f;
        float verticalForce = playerController.jumpPower;

        rb.AddForce(horizontalDirection.normalized * pushForce, ForceMode.Impulse);
        rb.AddForce(Vector3.up * verticalForce, ForceMode.Impulse);

        StartCoroutine(ClearRepulseVelocity());
        // rb.linearVelocity = horizontalDirection.normalized * pushForce + Vector3.up * verticalForce;
    }

    /// <summary>
    /// Waits until the player lands (is grounded) and then zeroes out horizontal velocity.
    /// </summary>
    private IEnumerator ClearRepulseVelocity()
    {
        // Wait until the player is grounded
        while (!playerController.GetIsGrounded())
        {
            yield return null;
        }
        // Small delay to let the landing settle
        yield return new WaitForSeconds(0.01f);

        // Clear horizontal velocity (keeping any vertical component)
        Vector3 currentVel = rb.linearVelocity;
        currentVel.x = 0;
        currentVel.z = 0;
        rb.linearVelocity = currentVel;
    }
}

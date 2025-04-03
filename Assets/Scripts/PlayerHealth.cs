using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a player
/// version 1.0
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 2f; // Adjusted default, tune as needed
    [SerializeField] private float knockbackAngle = 45f; // Adjusted default, tune as needed
    [SerializeField] private float invincibilityDuration = 2f;
    private static int health;
    private bool isInvincible;
    private Rigidbody rb;
    private PlayerController playerController;

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

        if (rb == null) Debug.LogError("PlayerHealth: Rigidbody component not found!");
        if (playerController == null) Debug.LogError("PlayerHealth: PlayerController component not found!");
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
            //FindFirstObjectByType<LoseMenu>().GameLost();
        }
    }

    /// <summary>
    /// TakeDamage method 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage, Vector3 spikePos)
    {
        PushBackFromSpike(spikePos);

        if(!isInvincible)
        {
            health = health - damage;
            ValidateHealth();
            BecomeInvincible(2f);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeDurationSec"></param>
    public void BecomeInvincible(float timeDurationSec)
    {
        if(!isInvincible)
        {
            SetInvincible();
            StartCoroutine(ResumeVincibility(timeDurationSec));
        }
    }
    
    /// <summary>
    /// Waits for a num of seconds, then resumes vincibility.
    /// </summary>
    /// <param name="health"></param>
    /// <returns></returns>
    private IEnumerator ResumeVincibility(float timeDurationSec)
    {
        Debug.Log("INVINCIBLE START");
        yield return new WaitForSeconds(timeDurationSec);
        SetVincible();
        Debug.Log("NOT INVINCIBLE");
    }

    /// <summary>
    /// Push the player back to the last tile stepped position.
    /// </summary>
    private void PushBackFromSpike(Vector3 spikePos)
    {
        if (rb == null || playerController == null) return;

        Vector3 impactDirectionHorizontal = transform.position - spikePos;
        impactDirectionHorizontal.y = 0;
        impactDirectionHorizontal.Normalize();

        float radianAngle = knockbackAngle * Mathf.Deg2Rad;
        Vector3 verticalComponent = Vector3.up * Mathf.Sin(radianAngle);
        Vector3 knockbackVector = (impactDirectionHorizontal + verticalComponent).normalized * knockbackForce;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(knockbackVector, ForceMode.Impulse);
        playerController.SetDisableAutoLaunch(true);

        Debug.Log($"Knockback applied. Force: {knockbackVector.magnitude}");

        // float knockbackForce = 3f; // needs to access player obj's PlayerController.
        // float knockbackAngle = 45f;

        // float radianAngle = knockbackAngle * Mathf.Deg2Rad;
        // Vector3 knockbackVector = new Vector3(impactDirection.x, Mathf.Sin(radianAngle), impactDirection.z) * knockbackForce;

        // // Apply force
        // rb.linearVelocity = Vector3.zero; // Reset velocity
        // rb.AddForce(knockbackVector, ForceMode.Impulse);

        // PlayerController controller = GetComponent<PlayerController>();
        // if(controller != null)
        // {
        //     controller.isKnockback = true;
        //     controller.SetDisableAutoLaunch(true);
        // }
    }
}

using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a player
/// version 1.1
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 2f;
    [SerializeField] private float knockbackAngle = 45f;
    [SerializeField] private Color flickerColor;
    public AudioSource audioSource;
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
        if(GameManager.Instance != null)
        {
            health = GameManager.Instance.currentHealth;
        }
        else
        {
            Debug.LogError("Failed to access currentHealth from GameManager singleton.");
        }

        isInvincible = false;
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        flashBehaviour = GetComponent<FlashBehaviour>();

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
            FindFirstObjectByType<LoseMenu>().GameLost();
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
            audioSource.Play();
            health = health - damage;
            ValidateHealth();
            DamageFlicker();
            BecomeInvincible(2f);
        }
    }

    /// <summary>
    /// Damage flicker calls the coroutine flicker
    /// </summary> 
    private void DamageFlicker()
    {
        if (flashBehaviour != null)
        {
            StartCoroutine(Flicker());
        }    
    }

    /// <summary>
    /// Flicker coroutine.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Flicker()
    {
        if (flashBehaviour != null)
        {
           yield return flashBehaviour.FlickerCoroutine(0.5f, flickerColor);
        }
    }

    /// <summary>
    /// Become invincible for a certain amount of time.
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
    /// Become invincible for a certain amount of time and flash the player.
    /// </summary>
    /// <param name="timeDurationSec"></param>
    /// <param name="flashColor"></param>
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
        Debug.Log("INVINCIBLE START");
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
    }
}

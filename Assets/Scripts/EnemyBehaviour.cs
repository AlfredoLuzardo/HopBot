using UnityEngine;

/// <summary>
/// Author: Keith Chow A01405612
/// Author: Alfredo Luzardo A01379913
/// References: https://www.youtube.com/watch?v=9eTZqxxgGz8
/// Implements the enemy behaviour.
/// </summary>
public class EnemyBehaviour : MonoBehaviour, Harmful
{
    public GameObject Target;
    public float distance;
    public float speed = 1f;
    private bool isAllowed;
    private Rigidbody rb;
    private MapManager mapManager;
    private int damage = 1;

    /// <summary>
    /// Getter for isAllowed
    /// </summary>
    /// <returns>isAllowed</returns>
    public bool GetIsAllowed() => isAllowed;

    /// <summary>
    /// Setter for isAllowed to false
    /// </summary>
    public bool SetNotAllowed() => isAllowed = false;

    /// <summary>
    /// Setter for isAllowed to true;
    /// </summary>
    public bool SetAllowed() => isAllowed = true;

    /// <summary>
    /// Initializes the Rigidbody.
    /// </summary>
    public void Start()
    {
        mapManager = FindFirstObjectByType<MapManager>();
        
        if(mapManager.GetPlayerInstance() != null)
        {
            Target = mapManager.GetPlayerInstance();
        }
        
        rb = GetComponent<Rigidbody>();
        
        isAllowed = true;
    }
    
    /// <summary>
    /// Follows the player character every frame.
    /// </summary>
    public void Update()
    {
        if(isAllowed)
        {
            Vector3 direction;
            
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 1f * Time.deltaTime);
            direction = (Target.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Detects when the player collides with the spike.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attack(collision.gameObject);
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
            health.TakeDamage(damage);
            Debug.Log("PLAYER HEALTH AFTER: " + health.GetHealth());
        }
        else
        {
            Debug.LogWarning("Player does not have PlayerHealth component!");
        }
    }
}

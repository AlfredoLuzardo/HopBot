using UnityEngine;

/// <summary>
/// Author: Keith Chow A01405612
/// Author: Alfredo Luzardo A01379913
/// References: https://www.youtube.com/watch?v=9eTZqxxgGz8
/// Implements the enemy behaviour.
/// version 1.5
/// </summary>
public class EnemyBehaviour : MonoBehaviour, Harmful
{
    [SerializeField] ParticleSystem EmpParticle;
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
    public void SetNotAllowed()
    {
        isAllowed = false;
    }

    /// <summary>
    /// Setter for isAllowed to true;
    /// </summary>
    public void SetAllowed() => isAllowed = true;

    /// <summary>
    /// Getter for particleSystem
    /// </summary>
    /// <returns></returns>
    public ParticleSystem GetEmpParticle()
    {
        return EmpParticle;
    }

    /// <summary>
    /// Initializes the Rigidbody.
    /// </summary>
    public void Start()
    {
        EmpParticle = transform.GetChild(14).GetComponent<ParticleSystem>();
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
        PreventTripping();
        if(isAllowed)
        {
            Vector3 direction;
            Quaternion lookRotation;

            direction = (Target.transform.position - transform.position).normalized;
            direction.y = 0f;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            direction = (Target.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Prevent the player falling down
    /// </summary>
    private void PreventTripping()
    {
        Quaternion currentRotation;
        
        currentRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
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

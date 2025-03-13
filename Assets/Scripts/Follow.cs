using UnityEngine;

/// <summary>
/// Follows the player character.
/// </summary>
public class Follow : MonoBehaviour
{
    public GameObject Target;
    public float distance;
    public float speed = 3f;
    private Rigidbody rb;
    private MapManager mapManager;
    
    /// <summary>
    /// Initializes the Rigidbody.
    /// </summary>
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        if(mapManager.GetPlayerInstance() != null)
        {
            Target = mapManager.GetPlayerInstance();
        }
        rb = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// Follows the player character every frame.
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 3 * Time.deltaTime);
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        Debug.Log($"target pos: {Target.transform.position}");
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }
}

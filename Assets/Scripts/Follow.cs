using UnityEngine;

/// <summary>
/// Author: Keith Chow A01405612
/// Author: Alfredo Luzardo A01379913
/// References: https://www.youtube.com/watch?v=9eTZqxxgGz8
/// Follows the player character.
/// </summary>
public class Follow : MonoBehaviour
{
    public GameObject Target;
    public float distance;
    public float speed = 1f;
    private bool isAllowed;
    private Rigidbody rb;
    private MapManager mapManager;
    
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
    /// Follows the player character every frame.
    /// </summary>
    public void Update()
    {
        if(isAllowed)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 1f * Time.deltaTime);
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            // Debug.Log($"target pos: {Target.transform.position}");
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}

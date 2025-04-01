using System;
using UnityEngine;

/// <summary>
/// Author: Alex Choi
/// Handles player movement and launch mechanics.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public GameObject directionArrow;
    private Rigidbody rb;
    private Camera mainCamera;
    private bool isGrounded = false;
    private bool isJumping = false;
    
    /// <summary>
    /// Initializes the rigidbody, and the main camera
    /// </summary>
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    /// <summary>
    /// Updates the direction arrow, bot movement, and jump mechanics.
    /// </summary>
    public void Update()
    {
        PreventTripping();
        UpdateDirectionArrow();
        HandleBotMovement();

        if (isGrounded && !isJumping)
        {
            LaunchPlayer();
            Debug.Log("Jumped");
            isJumping = true;
        }
    }

    /// <summary>
    /// Keeps the direction arrow around the player and points it towards the mouse cursor.
    /// </summary>
    // void UpdateDirectionArrow()
    // {
    //     Ray ray;
    //     int layerMask;
    //     Vector3 targetPoint;
    //     Vector3 direction;

    //     ray       = mainCamera.ScreenPointToRay(Input.mousePosition);
    //     layerMask = ~LayerMask.GetMask("Player");

    //     // Default target is a point in front of the player
    //     targetPoint = transform.position + transform.forward * 10f;

    //     if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
    //     {
    //         targetPoint = hit.point;  // Use the point where the ray hit
    //     }

    //     // Determine the direction the arrow should face
    //     direction = targetPoint - transform.position;
    //     direction.y = 0; // Keep it horizontal

    //     if (direction.magnitude > 0.1f) // Prevent flickering when close to the player
    //     {
    //         // Make the arrow face the target
    //         directionArrow.transform.rotation = Quaternion.LookRotation(direction);

    //         // Make the player face the direction where the arrow is pointing
    //         transform.rotation = Quaternion.LookRotation(direction);
    //     }

    //     directionArrow.transform.position = transform.position; // Keep it centered on the player

    // }

    private void UpdateDirectionArrow()
    {
        Plane groundPlane;
        Ray ray;
        Vector3 targetPoint;
        Vector3 direction;
        
        groundPlane = new Plane(Vector3.up, transform.position); // Plane at player's height
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        targetPoint = transform.position + transform.forward * 10f; // Default target if no intersection

        if (groundPlane.Raycast(ray, out float enter))
        {
            targetPoint = ray.GetPoint(enter); // Project onto the plane
        }

        direction = targetPoint - transform.position;
        direction.y = 0; // Keep horizontal

        if (direction.sqrMagnitude > 0.5f) // Prevent flickering
        {
            directionArrow.transform.rotation = Quaternion.LookRotation(direction);
        }

        directionArrow.transform.position = transform.position + direction.normalized * 1f; // Slight offset forward
        transform.rotation = Quaternion.LookRotation(direction);
    }



    /// <summary>
    /// Moves the player towards the mouse cursor when Ctrl is held.
    /// </summary>
    // void HandleBotMovement()
    // {
    //     if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
    //     {
    //         // {
    //         //     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //         //     if (Physics.Raycast(ray, out RaycastHit hit))
    //         //     {
    //         //         Vector3 targetPosition = hit.point;
    //         //         // targetPosition.y = transform.position.y; // Keep movement on ground level
    //         //         transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
    //         //     }
    //         // }
    //         Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //         Vector3 targetPosition = transform.position + transform.forward * 10f; // Default to moving forward

    //         if (Physics.Raycast(ray, out RaycastHit hit))
    //         {
    //             targetPosition = hit.point;  // Use the point where the ray hit
    //         }

    //         // Make the player move towards the target position
    //         transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
        
    //     }
    // }

    private void HandleBotMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Plane groundPlane = new Plane(Vector3.up, transform.position);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            Vector3 targetPosition = transform.position + transform.forward * 10f; // Default movement target

            if (groundPlane.Raycast(ray, out float enter))
            {
                targetPosition = ray.GetPoint(enter); // Project onto the ground plane
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
        }
    }

    /// <summary>
    /// Prevent the player falling down
    /// </summary>
    private void PreventTripping()
    {
        Quaternion currentRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
    }

    /// <summary>
    /// Launches the player upwards with a fixed power of 1.
    /// </summary>
    void LaunchPlayer()
    {
        float power = 5;
        
        rb.AddForce(Vector3.up * power, ForceMode.Impulse); // Fixed jump force
        isGrounded = false;
    }

    /// <summary>
    /// Check if the player is contacting the tile
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tile stepped.");
        if(other.CompareTag("Tile"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}

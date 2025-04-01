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

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2f * Time.deltaTime);
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

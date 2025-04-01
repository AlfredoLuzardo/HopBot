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
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
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
    void UpdateDirectionArrow()
    {
        // {
        //     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //     int layerMask = ~LayerMask.GetMask("Player");

        //     if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        //     {
        //         // Determine the direction the arrow should face
        //         Vector3 direction = hit.point - transform.position;
        //         direction.y = 0; // Keep it horizontal

        //         if (direction.magnitude > 0.1f) // Prevent flickering when close to the player
        //         {
        //             // Make the arrow face the mouse cursor
        //             directionArrow.transform.rotation = Quaternion.LookRotation(direction);

        //             // Make the player face the direction where the arrow is pointing
        //             transform.rotation = Quaternion.LookRotation(direction);
        //         }
        //     }
        //     directionArrow.transform.position = transform.position; // Keep it centered on the player
        // }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        int layerMask = ~LayerMask.GetMask("Player");

        // Default target is a point in front of the player
        Vector3 targetPoint = transform.position + transform.forward * 10f;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            targetPoint = hit.point;  // Use the point where the ray hit
        }

        // Determine the direction the arrow should face
        Vector3 direction = targetPoint - transform.position;
        direction.y = 0; // Keep it horizontal

        if (direction.magnitude > 0.1f) // Prevent flickering when close to the player
        {
            // Make the arrow face the target
            directionArrow.transform.rotation = Quaternion.LookRotation(direction);

            // Make the player face the direction where the arrow is pointing
            transform.rotation = Quaternion.LookRotation(direction);
        }

        directionArrow.transform.position = transform.position; // Keep it centered on the player

    }


    /// <summary>
    /// Moves the player towards the mouse cursor when Ctrl is held.
    /// </summary>
    void HandleBotMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // {
            //     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //     if (Physics.Raycast(ray, out RaycastHit hit))
            //     {
            //         Vector3 targetPosition = hit.point;
            //         // targetPosition.y = transform.position.y; // Keep movement on ground level
            //         transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
            //     }
            // }
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPosition = transform.position + transform.forward * 10f; // Default to moving forward

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                targetPosition = hit.point;  // Use the point where the ray hit
            }

            // Make the player move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
        
        }
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

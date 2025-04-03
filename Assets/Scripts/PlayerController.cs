using System;
using UnityEngine;

/// <summary>
/// Author: Alex Choi
/// Handles player movement and launch mechanics.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float jumpPower = 5;
    public float movementSpeed = 2f;
    public GameObject directionArrow;
    public Vector3 lastTileStepped;
    private Rigidbody rb;
    private Camera mainCamera;
    private bool isJumping = false;
    private bool isGrounded = false;
    private int groundCount = 0;
    private bool disableAutoLaunch = false;
    
    /// <summary>
    /// Initializes the rigidbody, and the main camera
    /// </summary>
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        if (rb == null) Debug.LogError("PlayerController: Rigidbody component not found!");
        if (mainCamera == null) Debug.LogError("PlayerController: Main Camera not found!");
        if (directionArrow == null) Debug.LogError("PlayerController: Direction Arrow GameObject not assigned!");
    }

    /// <summary>
    /// Updates the direction arrow, bot movement, and jump mechanics.
    /// </summary>
    public void Update()
    {
        PreventTripping();
        UpdateDirectionArrow();
        HandleBotMovement();

        if (isGrounded && !isJumping && !disableAutoLaunch)
        {
            LaunchPlayer();
            isJumping = true;
        }

    }

    /// <summary>
    /// Keeps the direction arrow around the player and points it towards the mouse cursor.
    /// </summary>
    private void UpdateDirectionArrow()
    {
        Plane groundPlane;
        Ray ray;
        Vector3 targetPoint;
        Vector3 direction;
        
        groundPlane = new Plane(Vector3.up, transform.position);
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        targetPoint = transform.position + transform.forward * 10f;

        if (groundPlane.Raycast(ray, out float enter))
        {
            targetPoint = ray.GetPoint(enter);
        }

        direction = targetPoint - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.5f)
        {
            directionArrow.transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.rotation = Quaternion.LookRotation(direction);
    }

    /// <summary>
    /// Moves the player towards the mouse cursor when Ctrl is held.
    /// </summary>
    private void HandleBotMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Plane groundPlane;
            Ray ray;
            Vector3 targetPosition;

            groundPlane     = new Plane(Vector3.up, transform.position);
            ray             = mainCamera.ScreenPointToRay(Input.mousePosition);
            targetPosition = transform.position + transform.forward * 10f;

            if (groundPlane.Raycast(ray, out float enter))
            {
                targetPosition = ray.GetPoint(enter);
            }

            transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, movementSpeed * Time.deltaTime);
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
        if (rb == null) return;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false;
        groundCount = 0;
    }

    /// <summary>
    /// Check if the player is contacting the tile
    /// </summary>
    /// <param name="other">Mostly tiles</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tile"))
        {
            groundCount++;
            if(groundCount == 1)
            {
                isGrounded = true;
                isJumping = false;

                if (disableAutoLaunch)
                {
                    CancelInvoke(nameof(EnableAutoLaunch)); // Use nameof for safety
                    EnableAutoLaunch(); // Re-enable immediately
                }

                SafeTile safeTile = other.GetComponent<SafeTile>();

                if(safeTile != null)
                {
                    lastTileStepped = transform.position;
                }

                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero; // Also stop rotation
                }
            }
        }
    }

    /// <summary>
    /// Check if the player is leaving the tile
    /// </summary>
    /// <param name="other">Mostly tiles</param>
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Tile"))
        {
            groundCount--;
            if(groundCount <= 0)
            {
                groundCount = 0;
                isGrounded = false;
            }
        }
    }

    public void SetDisableAutoLaunch(bool value)
    {
        // Debug.Log($"SetDisableAutoLaunch called with: {value}");
        disableAutoLaunch = value;
        if (value)
        {
            CancelInvoke(nameof(EnableAutoLaunch)); // Cancel previous invokes if called rapidly
            Invoke(nameof(EnableAutoLaunch), 0.75f); // Increased delay slightly, adjust as needed
        }
        else
        {
             // If disabling is explicitly turned off, cancel any pending invoke too.
             CancelInvoke(nameof(EnableAutoLaunch));
        }
    }

    private void EnableAutoLaunch()
    {
        disableAutoLaunch = false;
    }
}

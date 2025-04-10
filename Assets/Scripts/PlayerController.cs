using System;
using UnityEngine;

/// <summary>
/// Author: Alex Choi
/// Handles player movement and launch mechanics.
/// version 1.0
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float jumpPower = 6;
    public float movementSpeed = 2f;
    public float fallThreshold = 10f;
    public GameObject directionArrow;
    public Vector3 lastTileStepped;
    public Vector3 currentPosition;
    public AudioSource audioSource;
    public AudioSource loseSound;
    private Rigidbody rb;
    private Camera mainCamera;
    public bool isJumping = false;
    public bool isGrounded = false;
    public int groundCount = 0;
    private bool isLost = false;
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
        CheckGameOverFallOFf();

        if (isGrounded && !isJumping && !disableAutoLaunch)
        {
            LaunchPlayer();
            isJumping = true;
        }
    }

    /// <summary>
    /// Ends the game if player falls off the map
    /// </summary>
    private void CheckGameOverFallOFf()
    {
        if(!isLost)
        {
            currentPosition = transform.position;
            if(currentPosition.y <= fallThreshold)
            {
                isLost = true;
                LoseMenu loseMenu = FindFirstObjectByType<LoseMenu>();
                loseMenu.GameLost();
            }
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
    /// Moves the player towards the mouse cursor when Ctrl or mouse left button is held.
    /// </summary>
    private void HandleBotMovement()
    {
        if (Input.GetMouseButton(0) ||
            Input.GetKey(KeyCode.LeftControl) ||
            Input.GetKey(KeyCode.RightControl))
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
    public void LaunchPlayer()
    {
        if (rb == null) return;
        audioSource.Play();
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false;
        groundCount = 0;
    }

    /// <summary>
    /// Check if the player is contacting the tile
    /// </summary>
    /// <param name="other">Mostly tiles</param>
    public void OnTriggerEnter(Collider other)
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
                    CancelInvoke(nameof(EnableAutoLaunch)); 
                    EnableAutoLaunch(); 
                }

                SafeTile safeTile = other.GetComponent<SafeTile>();

                if(safeTile != null)
                {
                    lastTileStepped = transform.position;
                }

                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero; 
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

    /// <summary>
    /// Sets the disableAutoLaunch variable to true or false.
    /// </summary>
    /// <param name="value"></param>
    public void SetDisableAutoLaunch(bool value)
    {
        disableAutoLaunch = value;
        if (value)
        {
            CancelInvoke(nameof(EnableAutoLaunch));
            Invoke(nameof(EnableAutoLaunch), 0.75f);
        }
        else
        {
             CancelInvoke(nameof(EnableAutoLaunch));
        }
    }

    /// <summary>
    /// Enables auto launch
    /// </summary>
    private void EnableAutoLaunch()
    {
        disableAutoLaunch = false;
    }
}

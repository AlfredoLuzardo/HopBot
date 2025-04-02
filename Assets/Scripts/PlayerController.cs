using System;
using UnityEngine;

/// <summary>
/// Author: Alex Choi
/// Handles player movement and launch mechanics.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float jumpPower = 5;
    public GameObject directionArrow;
    private Rigidbody rb;
    private Camera mainCamera;
    private bool isJumping = false;
    private bool isGrounded = false;
    private int groundCount = 0;
    
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
        Debug.Log("ground count: " + groundCount);

        if (isGrounded && !isJumping)
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

        directionArrow.transform.position = transform.position + direction.normalized * 1f;
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

            Vector3 targetPosition = transform.position + transform.forward * 10f;

            if (groundPlane.Raycast(ray, out float enter))
            {
                targetPosition = ray.GetPoint(enter);
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
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false;
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
}

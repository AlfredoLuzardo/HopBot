using UnityEngine;

public class PlayerLaunchController : MonoBehaviour
{
    public Transform player;          // The player object (the center of rotation)
    public Transform directionArrow;  // The UI arrow that rotates around the player
    public float rotationDuration = 1.5f;  // Time to complete the full rotation
    private bool isRotating = false;
    private Vector3 savedDirection;

    private float rotationTime = 0f;

    void Start()
    {
        // Ensure the arrow is initially hidden
        directionArrow.gameObject.SetActive(false);

        // Set the initial orientation of the arrow to lie flat on the XY plane (horizontal)
        directionArrow.rotation = Quaternion.Euler(0, 0, 0); // No tilt
    }

    void Update()
    {
        // Listen for mouse click (First and Second Clicks)
        if (Input.GetMouseButtonDown(0))
        {
            if (!isRotating)
            {
                // First Click: Start rotating the arrow
                isRotating = true;
                directionArrow.gameObject.SetActive(true);
                rotationTime = 0f; // Reset rotation time
            }
            else
            {
                // Second Click: Save direction and stop rotating
                savedDirection = directionArrow.position - player.position;
                directionArrow.gameObject.SetActive(false);
                isRotating = false;
            }
        }

        // If the arrow is rotating, update its position based on rotation time
        if (isRotating)
        {
            RotateArrow();
        }
    }

    // Rotate the arrow clockwise around the player
    void RotateArrow()
    {
        rotationTime += Time.deltaTime; // Increment time by deltaTime

        // Calculate the rotation amount as a fraction of a full circle
        float rotationFraction = rotationTime / rotationDuration;
        rotationFraction = rotationFraction % 1f;  // Ensure it loops back after 1.0

        // Convert the fraction to an angle in radians
        float angle = rotationFraction * 360f;

        // Position the arrow at a distance from the player, rotating around the player (clockwise)
        // Keep the tail fixed at the player's position
        Vector3 newPos = player.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, -Mathf.Sin(Mathf.Deg2Rad * angle)) * 2f;

        // Debug log to check position
        Debug.Log("New Position: " + newPos);

        directionArrow.position = newPos;

        // Make the arrow always face in the direction of rotation, so it points away from the player
        directionArrow.LookAt(player); // Make the arrow face the player
    }
}

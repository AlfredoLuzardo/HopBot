using UnityEngine;

/// <summary>
/// Author: Alex Choi A01323994
/// Controls the arrow to represent the launch angle. 
/// The arrow oscillates between 0 and 90 degrees, indicating the vertical launch angle.
/// </summary>
public class ArrowAngle : MonoBehaviour
{
    public float angleSpeed;
    private float angle = 0f;
    private bool increasing = true;
    private Vector3 baseDirection = Vector3.forward;
    private float yDirection;

    /// <summary>
    /// Sets the base Y-axis rotation for the angle arrow to align with the saved launch direction.
    /// </summary>
    public void SetBaseDirection(float y)
    {
        yDirection = y;
        Quaternion ySet = Quaternion.Euler(0, yDirection, 0);
        transform.rotation = Quaternion.LookRotation(baseDirection) * ySet;
    }

    /// <summary>
    /// Updates the arrow's rotation to oscillate between 0 and 90 degrees, simulating angle selection.
    /// </summary>
    void Update()
    {
        if (increasing)
        {
            angle += angleSpeed * Time.deltaTime;
            if (angle >= 90f) increasing = false;
        }
        else
        {
            angle -= angleSpeed * Time.deltaTime;
            if (angle <= 0f) increasing = true;
        }

        Quaternion yRotation = Quaternion.Euler(-angle, yDirection, 0);
        transform.rotation = Quaternion.LookRotation(baseDirection) * yRotation;
    }
}
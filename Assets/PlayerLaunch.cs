using System;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    public GameObject directionArrow;
    public GameObject angleArrow;
    public GameObject gaugeArrow;

    private Vector3 savedDirection;
    private float savedXRotation;
    private float savedYRotation;
    private float savedPower;
    private int clickCount;
    private Rigidbody rb;

    /// <summary>
    /// Initializes the Rigidbody and resets the click count.
    /// Adds error handling in case the Rigidbody is not attached.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the GameObject.");
        }
        clickCount = 0;
    }

    /// <summary>
    /// Handles user input to control the launch process across three stages: direction, angle, and power.
    /// Continuously updates the player’s rotation based on saved values.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;

            if (clickCount == 1)
            {
                savedYRotation = directionArrow.transform.eulerAngles.y;
                Debug.Log($"Saved Y: {savedYRotation}");
                angleArrow.GetComponent<ArrowAngle>().SetBaseDirection(savedYRotation);

                directionArrow.SetActive(false);
                angleArrow.SetActive(true);
            }
            else if (clickCount == 2)
            {
                savedXRotation = angleArrow.transform.eulerAngles.x;
                Debug.Log($"Saved X: {savedXRotation}");
                gaugeArrow.GetComponent<ArrowGauge>().SetBaseDirection(savedXRotation, savedYRotation);

                angleArrow.SetActive(false);
                gaugeArrow.SetActive(true);
            }
            else if (clickCount == 3)
            {
                savedPower = gaugeArrow.GetComponent<ArrowGauge>().getScaleFromOriginal();
                Debug.Log($"Power:{savedPower}");
                LaunchPlayer();

                gaugeArrow.SetActive(false);
                directionArrow.SetActive(true);
            }
        }

        Quaternion baseRotation = Quaternion.LookRotation(Vector3.forward);
        Quaternion yRotation = Quaternion.Euler(0, savedYRotation, 0);
        transform.rotation = baseRotation * yRotation;
    }

    /// <summary>
    /// Calculates launch direction and applies force to the Rigidbody.
    /// Resets launch parameters after applying the force.
    /// </summary>
    void LaunchPlayer()
    {
        float angleInRadians = savedXRotation * Mathf.Deg2Rad;
        float posY = Mathf.Abs(360 * Mathf.Sin(angleInRadians));
        float posZ = 360 * Mathf.Cos(angleInRadians);

        Debug.Log($"posY, posZ: {posY}, {posZ}");

        savedDirection = new Vector3(0, posY, posZ);
        rb.AddRelativeForce(savedDirection * savedPower, ForceMode.Impulse);

        savedXRotation = 0f;
        savedPower = 0f;
        clickCount = 0;
    }
}

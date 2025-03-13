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
    private int clickCount = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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

        transform.rotation = Quaternion.LookRotation(
            Vector3.forward
        ) * Quaternion.Euler(0, savedYRotation, 0);
    }

    void LaunchPlayer()
    {
        transform.rotation = Quaternion.Euler(0.0f, savedYRotation, 0.0f);

        float posY = Mathf.Abs(360 * Mathf.Sin(savedXRotation * Mathf.Deg2Rad));
        float posZ = 360 * Mathf.Cos(savedXRotation * Mathf.Deg2Rad);

        Debug.Log($"posY, posZ: {posY}, {posZ}");


        savedDirection = new Vector3(
            0, posY, posZ
        );

        rb.AddRelativeForce(savedDirection * savedPower, ForceMode.Impulse);

        // savedDirection = new Vector3(0, 0, 0);
        savedXRotation = 0f;
        savedPower = 0f;
        clickCount = 0;
    }
}

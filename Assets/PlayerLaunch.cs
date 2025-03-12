using System;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    public GameObject directionArrow;
    public GameObject angleArrow;
    public GameObject gaugeArrow;
    public float launchPower;

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
                angleArrow.GetComponent<ArrowAngle>().SetBaseDirection(savedYRotation);

                directionArrow.SetActive(false);
                angleArrow.SetActive(true);
            }
            else if (clickCount == 2)
            {
                savedXRotation = angleArrow.transform.eulerAngles.x;
                gaugeArrow.GetComponent<ArrowGauge>().SetBaseDirection(savedXRotation, savedYRotation);
                
                angleArrow.SetActive(false);
                gaugeArrow.SetActive(true);
            }
            else if (clickCount == 3)
            {
                savedPower = gaugeArrow.GetComponent<ArrowGauge>().GetFillAmount();
                LaunchPlayer();

                savedDirection = new Vector3();
                savedXRotation = 0f;
                savedYRotation = 0f;
                savedPower = 0f;
                clickCount = 0;
                gaugeArrow.SetActive(false);
                directionArrow.SetActive(true);
            }
        }
    }

    void LaunchPlayer()
    {
        savedDirection = new Vector3(
            savedXRotation, savedYRotation, 0
        ) * savedPower;

        rb.linearVelocity = savedDirection;
    }
}

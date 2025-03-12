using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    public GameObject directionArrow;
    public GameObject angleArrow;
    public GameObject gaugeArrow;
    public float launchPower;

    private Vector3 savedDirection;
    private float savedAngle;
    private float savedPower;
    private int clickCount = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;

            if (clickCount == 1)
            {
                savedDirection = directionArrow.transform.forward;
                angleArrow.transform.position = directionArrow.transform.position;
                angleArrow.transform.rotation = Quaternion.LookRotation(savedDirection);

                directionArrow.SetActive(false);
                angleArrow.SetActive(true);
            }
            else if (clickCount == 2)
            {
                savedAngle = angleArrow.transform.localEulerAngles.y;
                angleArrow.SetActive(false);
                gaugeArrow.SetActive(true);
            }
            else if (clickCount == 3)
            {
                savedPower = gaugeArrow.GetComponent<ArrowGauge>().GetFillAmount();
                gaugeArrow.SetActive(false);
                LaunchPlayer();
            }
        }
    }

    void LaunchPlayer()
    {
        Quaternion rotation = Quaternion.Euler(0, savedAngle, 0);
        Vector3 launchDirection = rotation * savedDirection;

        GetComponent<Rigidbody>().AddForce(launchDirection * savedPower * launchPower, ForceMode.Impulse);
    }
}

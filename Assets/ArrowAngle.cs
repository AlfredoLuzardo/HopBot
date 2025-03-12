using UnityEngine;

public class ArrowAngle : MonoBehaviour
{
    public float angleSpeed;
    private float angle = 0f;
    private bool increasing = true;

    void Update()
    {
        if (increasing)
        {
            angle += angleSpeed * Time.deltaTime;
            if (angle >= -0f) increasing = false;
        }
        else
        {
            angle -= angleSpeed * Time.deltaTime;
            if (angle <= -90f) increasing = true;
        }

        transform.localEulerAngles = new Vector3(angle, 0, -90);
    }
}

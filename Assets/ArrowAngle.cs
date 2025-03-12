using UnityEngine;

public class ArrowAngle : MonoBehaviour
{
    public float angleSpeed;
    private float angle = 0f;
    private bool increasing = true;
    private Vector3 baseDirection = Vector3.forward;
    private float yDirection;

    public void SetBaseDirection(float y)
    {
        yDirection = y;
        Quaternion ySet = Quaternion.Euler(0, yDirection, 0);
        transform.rotation = Quaternion.LookRotation(baseDirection) * ySet;
    }

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

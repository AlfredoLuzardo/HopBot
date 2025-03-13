using UnityEngine;

public class ArrowGauge : MonoBehaviour
{
    public float powerSpeed;

    private Vector3 baseDirection = Vector3.forward;
    private float xDirection;
    private float yDirection;

    private float scaleFromOriginal = 0f;
    private bool increasing = true;

    public void SetBaseDirection(float x, float y)
    {
        xDirection = x;
        yDirection = y;
        Quaternion xySet = Quaternion.Euler(xDirection, yDirection, 0);
        transform.rotation = Quaternion.LookRotation(baseDirection) * xySet;
    }

    void Update()
    {
        Quaternion xyRotation = Quaternion.Euler(xDirection, yDirection, 0);
        transform.rotation = Quaternion.LookRotation(baseDirection) * xyRotation;

        if(increasing)
        {
            scaleFromOriginal += powerSpeed * Time.deltaTime;
            if(scaleFromOriginal >= 1f)
            {
                increasing = false;
            }
        }
        else
        {
            scaleFromOriginal -= powerSpeed * Time.deltaTime;
            if(scaleFromOriginal <= 0f)
            {
                increasing = true;
            }
        }

        transform.localScale = new Vector3(1, 1, scaleFromOriginal);
    }

    public float getScaleFromOriginal()
    {
        return scaleFromOriginal;
    }
}

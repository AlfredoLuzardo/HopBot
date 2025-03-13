using UnityEngine;

/// <summary>
/// Author: Alex Choi A01323994
/// Controls the arrow that represents the launch power gauge.
/// The arrow scales back and forth to visualize power selection.
/// </summary>
public class ArrowGauge : MonoBehaviour
{
    public float powerSpeed;

    private Vector3 baseDirection = Vector3.forward;
    private float xDirection;
    private float yDirection;

    private float scaleFromOriginal = 0f;
    private bool increasing = true;

    /// <summary>
    /// Sets the base rotation of the gauge arrow based on the selected launch angle and direction.
    /// </summary>
    /// <param name="x">X-axis rotation representing the launch angle.</param>
    /// <param name="y">Y-axis rotation representing the launch direction.</param>
    public void SetBaseDirection(float x, float y)
    {
        xDirection = x;
        yDirection = y;
        Quaternion xySet = Quaternion.Euler(xDirection, yDirection, 0);
        transform.rotation = Quaternion.LookRotation(baseDirection) * xySet;
    }

    /// <summary>
    /// Updates the arrow's scale to oscillate between 0 and 1, representing power selection.
    /// </summary>
    void Update()
    {
        Quaternion xyRotation = Quaternion.Euler(xDirection, yDirection, 0);
        transform.rotation = Quaternion.LookRotation(baseDirection) * xyRotation;

        if (increasing)
        {
            scaleFromOriginal += powerSpeed * Time.deltaTime;
            if (scaleFromOriginal >= 1f)
            {
                increasing = false;
            }
        }
        else
        {
            scaleFromOriginal -= powerSpeed * Time.deltaTime;
            if (scaleFromOriginal <= 0f)
            {
                increasing = true;
            }
        }

        transform.localScale = new Vector3(1, 1, scaleFromOriginal);
    }

    /// <summary>
    /// Gets the current scale of the gauge, representing the selected power level.
    /// </summary>
    /// <returns>The selected power level as a float between 0 and 1.</returns>
    public float getScaleFromOriginal()
    {
        return scaleFromOriginal;
    }
}

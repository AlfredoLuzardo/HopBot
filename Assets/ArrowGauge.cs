using UnityEngine;

public class ArrowGauge : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float gaugeSpeed;
    private float fillValue = 0f;
    private bool increasing = true;

    void Update()
    {
        // Fluctuate between 0 and 1
        if (increasing)
        {
            fillValue += gaugeSpeed * Time.deltaTime;
            if (fillValue >= 1f) increasing = false;
        }
        else
        {
            fillValue -= gaugeSpeed * Time.deltaTime;
            if (fillValue <= 0f) increasing = true;
        }

        // Change transparency from tail (0) to head (1)
        Color color = spriteRenderer.color;
        color.a = fillValue;
        spriteRenderer.color = color;
    }

    public float GetFillAmount()
    {
        return fillValue; // Returns percentage
    }
}

using UnityEngine;

public class ArrowGauge : MonoBehaviour
{
    public float colorSpeed;

    private Vector3 baseDirection = Vector3.forward;
    private float xDirection;
    private float yDirection;

    private float fillAmount = 0f;
    private MaterialPropertyBlock propBlock;
    private Renderer rend;
    private bool increasing = true;

    public void Awake()
    {
        rend = GetComponent<Renderer>();
        propBlock = new MaterialPropertyBlock();
    }

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
            fillAmount += colorSpeed * Time.deltaTime;
            if(fillAmount >= 1f)
            {
                increasing = false;
            }
        }
        else
        {
            fillAmount -= colorSpeed * Time.deltaTime;
            if(fillAmount <= 0f)
            {
                increasing = true;
            }
        }

        rend.GetPropertyBlock(propBlock);
        Color color = Color.Lerp(Color.white, Color.red, fillAmount);
        propBlock.SetColor("_Color", color);
        rend.SetPropertyBlock(propBlock);
    }

    public float GetFillAmount()
    {
        return fillAmount;
    }
}

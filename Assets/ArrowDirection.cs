using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    public Transform player;
    public float orbitSpeed;

    void Update()
    {
        transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}

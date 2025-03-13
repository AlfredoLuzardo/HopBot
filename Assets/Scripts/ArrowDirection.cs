using UnityEngine;

/// <summary>
/// Rotates the arrow around the player to indicate the launch direction.
/// The arrow orbits the player on the Y-axis at a constant speed.
/// </summary>
public class ArrowDirection : MonoBehaviour
{
    public Transform player;
    public float orbitSpeed;

    /// <summary>
    /// Rotates the arrow around the player on the Y-axis at a constant speed, 
    /// representing the direction selection phase.
    /// </summary>
    void Update()
    {
        transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}

using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the invincibility item
/// version 1.0
/// </summary>
public class Invincibility : Item
{
    private float duration = 2f;
    private Collider itemCollider;
    private MeshRenderer itemRenderer;

    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {
        itemCollider = GetComponent<Collider>();
        itemRenderer = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Detects when the player collides with the invincibility item.
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            
            itemCollider.enabled = false;
            itemRenderer.enabled = false;
        }
    }
}

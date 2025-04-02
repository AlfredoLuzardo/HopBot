using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the invincibility item
/// References: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.StartCoroutine.html
///             https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Coroutine.html 
// version 1.2
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
        Transform child;

        child = transform.GetChild(0);
        itemCollider = GetComponent<Collider>();

        if(child != null)
        {
            itemRenderer = child.GetComponent<MeshRenderer>();
        }
    }

    /// <summary>
    /// Detects when the player collides with the invincibility item.
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth health;

            health = other.gameObject.GetComponent<PlayerHealth>();

            health.SetInvincible();

            Debug.Log("INVINCIBLE");

            StartCoroutine(ResumeVincibility(health));
            
            itemCollider.enabled = false;
            itemRenderer.enabled = false;
        }
    }

    /// <summary>
    /// Waits for a num of seconds, then resumes vincibility.
    /// </summary>
    /// <param name="health"></param>
    /// <returns></returns>
    private IEnumerator ResumeVincibility(PlayerHealth health)
    {
        yield return new WaitForSeconds(duration);

        health.SetVincible();

        Debug.Log("NOT INVINCIBLE");
    }
}

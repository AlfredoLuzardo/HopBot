using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the StopEnemies item
/// References: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.StartCoroutine.html
///             https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Coroutine.html
/// version 1.1
/// </summary>
public class StopEnemies : Item
{
    private float stopDuration = 3f;
    private Collider itemCollider;
    private MeshRenderer itemRenderer;

    private void Start()
    {
        itemCollider = GetComponent<Collider>();
        itemRenderer = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Detects when the player collides with the StopEnemies item.
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Follow[] enemies;

            enemies = FindObjectsOfType<Follow>();

            foreach(Follow enemy in enemies)
            {
                enemy.SetNotAllowed();
            }

            StartCoroutine(ResumeEnemies(enemies));

            itemCollider.enabled = false;
            itemRenderer.enabled = false;
        }
    }

    /// <summary>
    /// Yields for the number of seconds, then resumes the enemies
    /// </summary>
    /// <param name="enemies"></param>
    /// <returns></returns>
    private IEnumerator ResumeEnemies(Follow[] enemies)
    {
        yield return new WaitForSeconds(stopDuration);

        foreach(Follow enemy in enemies)
        {
            enemy.SetAllowed();
        }
    }
}

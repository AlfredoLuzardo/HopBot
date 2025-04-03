using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the flash behaviour
/// References: https://www.youtube.com/watch?v=DVSBkN37MR0&t=91s&ab_channel=KetraGames
/// version 1.0
/// </summary>
public class FlashBehaviour : MonoBehaviour
{
    private List<Renderer> modelRenderer;

    /// <summary>
    /// Triggers on awake. 
    /// </summary>
    private void Awake()
    {
        GameObject model;

        model = transform.GetChild(0).gameObject;
        modelRenderer = new List<Renderer>();

        foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>())
        {
            if (renderer != null)
            {
                modelRenderer.Add(renderer);
            }
        }
    }

    /// <summary>
    /// Flashes the model for a given duration, interval and color.
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="color"></param>
    /// <param name="flashSpeed"></param>
    /// <returns></returns>
    public IEnumerator FlashCouroutine(float duration, Color color, float flashSpeed)
    {
        float elapsedTime;
        List<Color> originalColors;

        elapsedTime = 0f;
        originalColors = new List<Color>();

        foreach (Renderer renderer in modelRenderer)
        {
            originalColors.Add(renderer.material.color);
        }

        while (elapsedTime < duration)
        {
            float t;
            
            t = Mathf.PingPong(Time.time * (1f / flashSpeed), 1f);

            for (int i = 0; i < modelRenderer.Count; i++)
            {
                if (modelRenderer[i] != null)
                {
                    modelRenderer[i].material.color = Color.Lerp(originalColors[i], color, t);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Renderer renderer in modelRenderer)
        {
            if (renderer != null)
            {
                renderer.material.color = originalColors[modelRenderer.IndexOf(renderer)];
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Author: Alex Choi
/// Represents a level
/// version 1.0
/// </summary>
public class Level : MonoBehaviour
{
    public Text levelText;
    
    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        UpdateLevelText();
    }

    /// <summary>
    /// Updates the level text corresponding to the level
    /// </summary> 
    void UpdateLevelText()
    {
        if (levelText != null && GameManager.Instance != null)
        {
            levelText.text = "Level: " + GameManager.Instance.currentLevel;
        }
         else if (levelText == null)
        {
            Debug.LogError("Level Text component not assigned!", this);
        }
         else
        {
            Debug.LogWarning("GameManager not ready yet for LevelDisplay.");
            levelText.text = "Level: ?";
        }
    }
}

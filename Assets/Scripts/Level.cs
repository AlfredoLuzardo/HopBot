using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    // private int currentLevel = 0;
    // [SerializeField] Text levelText;
    // void Start()
    // {
    //     MainMenu mainMenu = GetComponent<MainMenu>();
    //     currentLevel = mainMenu.currentLevel;
    // }

    // void Update()
    // {
    //     levelText.text = string.Format("Level {0}", currentLevel);
    // }
    public Text levelText; // Assign your legacy Text component here
    // public TextMeshProUGUI levelText; // Or Assign your TextMeshPro component

    void Start()
    {
         // Initial display
         UpdateLevelText();
    }

    // Update can be inefficient if called every frame.
    // Call UpdateLevelText() when the scene loads or when the level might change.
    // Start() is usually sufficient if the level only changes between scenes.
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
         else // GameManager.Instance == null
        {
             Debug.LogWarning("GameManager not ready yet for LevelDisplay.");
             // Optionally display default text or wait
             levelText.text = "Level: ?";
        }
    }
}

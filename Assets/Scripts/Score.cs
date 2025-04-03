using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // private int currentScore = 0;
    // [SerializeField] Text scoreText;
    
    // void Start()
    // {
    //     MainMenu mainMenu = GetComponent<MainMenu>();
    //     currentScore = mainMenu.currentGameScore;
    // }

    // void Update()
    // {
    //     scoreText.text = string.Format("Current Score: {0}", currentScore);
    // }
    public Text scoreText; // Assign your legacy Text component here
    // public TextMeshProUGUI scoreText; // Or Assign your TextMeshPro component

    void Update() // Score might change during gameplay or at the end, Update might be okay here, or use an event system.
    {
        if (scoreText != null && GameManager.Instance != null)
        {
            // Display the score stored in GameManager.
            // Make sure GameManager.currentGameScore represents what you want to show *during* the level.
            // If it only updates on win, you might need another variable for in-level score.
            scoreText.text = "Score: " + GameManager.Instance.currentGameScore;
        }
         else if (scoreText == null)
        {
            Debug.LogError("Score Text component not assigned!", this);
        }
        // No warning for GameManager here as score might legitimately be 0 or unavailable initially.
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int currentScore = 0;
    [SerializeField] Text scoreText;
    
    void Start()
    {
        MainMenu mainMenu = GetComponent<MainMenu>();
        currentScore = mainMenu.currentGameScore;
    }

    void Update()
    {
        scoreText.text = string.Format("Current Score: {0}", currentScore);
    }
}

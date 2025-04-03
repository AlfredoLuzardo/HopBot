using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private int currentLevel = 0;
    [SerializeField] Text levelText;
    void Start()
    {
        MainMenu mainMenu = GetComponent<MainMenu>();
        currentLevel = mainMenu.currentLevel;
    }

    void Update()
    {
        levelText.text = string.Format("Level {0}", currentLevel);
    }
}

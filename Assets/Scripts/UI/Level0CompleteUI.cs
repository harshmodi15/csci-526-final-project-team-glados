using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level0CompleteUI : MonoBehaviour
{
    public Text finalTimeText;

    void Start()
    {
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0f);
        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);
        finalTimeText.text = $"Your Time: {minutes:00}:{seconds:00}";
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene("baseLvl"); // Reload Level 1
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load Main Menu
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level2"); // Load the next level (replace with your scene name)
    }
}
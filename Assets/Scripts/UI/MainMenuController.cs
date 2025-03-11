using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadBaseLevel()
    {
        SceneManager.LoadScene("lvl0");
        FirebaseManager.instance.LogLevelStart(0);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("lvl1");
        PlayerStats.IncreaseLevelNumber();
        FirebaseManager.instance.LogLevelStart(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
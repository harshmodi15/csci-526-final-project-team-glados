using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadBaseLevel()
    {
        SceneManager.LoadScene("lvl0");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("lvl1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
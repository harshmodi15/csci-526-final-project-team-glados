using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadBaseLevel()
    {
        SceneManager.LoadScene("baselvl");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("jack_lvl");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
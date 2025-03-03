using UnityEngine;
using UnityEngine.SceneManagement; // Import Scene Management

public class MainMenuController : MonoBehaviour
{
    public void LoadBaseLevel()
    {
        SceneManager.LoadScene("baselvl"); // Change to the exact scene name
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("SampleScene"); // Change to the exact scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!"); // This will only show in the Unity editor
    }
}
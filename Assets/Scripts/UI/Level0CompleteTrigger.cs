using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0CompleteTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelTimer timer = FindObjectOfType<LevelTimer>();
            if (timer != null)
            {
                timer.StopTimer();
            }
            SceneManager.LoadScene("Level0Complete");
        }
    }
}
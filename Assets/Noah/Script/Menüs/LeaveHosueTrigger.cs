using UnityEngine;
using UnityEngine.SceneManagement;


public class LeaveHouseTrigger : MonoBehaviour
{
    public GameObject canvasPrompt; // Dein UI-Canvas

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasPrompt.SetActive(true);
            Time.timeScale = 0f; // Optional: Spiel pausieren
        }
    }

    public void OnClickYes()
    {
        Time.timeScale = 1f;
        // Szene mit Index +1 laden (nächste Szene)
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }


    public void OnClickNo()
    {
        canvasPrompt.SetActive(false);
        Time.timeScale = 1f;
    }
}

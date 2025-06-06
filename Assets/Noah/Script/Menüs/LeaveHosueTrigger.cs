using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LeaveHouseTrigger : MonoBehaviour
{
    public GameObject canvasPrompt;
    public AudioSource audioSource;
    public AudioClip clickSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasPrompt.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OnClickYes()
    {
        Time.timeScale = 1f;

        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        StartCoroutine(LoadNextSceneAfterDelay(1f));
    }

    public void OnClickNo()
    {
        canvasPrompt.SetActive(false);
        Time.timeScale = 1f;
        // Kein Sound hier – wie gewünscht
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}

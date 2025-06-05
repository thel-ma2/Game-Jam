using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(DelayedLoadScene(1));
    }

    public void GoToLevelMenu()
    {
        StartCoroutine(DelayedLoadScene(2));
    }

    public void GoToMainMenu()
    {
        StartCoroutine(DelayedLoadScene(0));
    }

    public void QuitGame()
    {
        StartCoroutine(DelayedQuitGame());
    }

    private IEnumerator DelayedLoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(1f); // 2 Sekunden warten
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator DelayedQuitGame()
    {
        yield return new WaitForSeconds(1f); // 2 Sekunden warten
        Application.Quit();
    }
}

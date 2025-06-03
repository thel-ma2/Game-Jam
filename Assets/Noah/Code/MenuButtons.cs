using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public void PlayGame()

    {
        SceneManager.LoadScene(1);
    }

    public void GoToLevelMenu()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void GoToMainMenu()

    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.Raise(new BeforeSceneChangeEvent(sceneName));
        }

        SceneManager.LoadScene(sceneName);
    }
}

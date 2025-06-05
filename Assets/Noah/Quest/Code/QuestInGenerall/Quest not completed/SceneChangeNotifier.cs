using UnityEngine;
using UnityEngine.SceneManagement;
//bermerkt den Scenen wechsel und gibt es anderen Scripts an
public class SceneChangeNotifier : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.Raise(new SceneChangedEvent(scene.name));
        }
    }
}

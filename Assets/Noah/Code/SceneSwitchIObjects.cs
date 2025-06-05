using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchIObjects : MonoBehaviour
{
    void Awake()
    {
        string objName = this.gameObject.name;

        if (objName == "BrunOutMeter" || objName == "Batterie")
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Direkt prüfen bei Start
            HandleSceneBehavior(SceneManager.GetActiveScene().name);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleSceneBehavior(scene.name);
    }

    private void HandleSceneBehavior(string sceneName)
    {
        if (sceneName == "Map")
        {
            // In der Map-Szene nur ausblenden
            this.gameObject.SetActive(false);
        }
        else if (sceneName == "Menu")
        {
            // In der Menu-Szene komplett zerstören
            Destroy(this.gameObject);
        }
        else
        {
            // In allen anderen Szenen anzeigen
            this.gameObject.SetActive(true);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

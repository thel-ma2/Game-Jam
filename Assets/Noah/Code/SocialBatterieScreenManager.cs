using UnityEngine;
using UnityEngine.SceneManagement;

public class SocialBatterieMinimumScreen : MonoBehaviour
{
    public SocialBatterie socialBatterie;    // Verweis auf dein SocialBatterie-Script
    public GameObject lowEnergyScreen;       // UI-Element, das angezeigt werden soll
    public string sceneToLoad = "";          // Name der Szene, die geladen werden soll
    public bool switchSceneAfterUI = false;  // Ob Szene nach UI angezeigt werden soll
    public float sceneSwitchDelay = 3f;      // Sekunden bis Szenenwechsel

    private bool triggered = false;

    void Update()
    {
        if (socialBatterie == null || lowEnergyScreen == null)
            return;

        if (socialBatterie.current <= socialBatterie.minimum && !triggered)
        {
            triggered = true;
            lowEnergyScreen.SetActive(true);
            Debug.Log("Minimum erreicht – UI aktiviert.");

            if (switchSceneAfterUI && !string.IsNullOrEmpty(sceneToLoad))
            {
                StartCoroutine(LoadSceneAfterDelay());
            }
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(sceneSwitchDelay);
        Debug.Log("Szene wird geladen: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}

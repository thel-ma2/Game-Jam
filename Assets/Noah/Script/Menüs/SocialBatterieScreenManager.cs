using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SocialBatterieMinimumScreen : MonoBehaviour
{
    public SocialBatterie socialBatterie;      // Wird automatisch von "Progress Bar" gesetzt
    public GameObject lowEnergyScreen;         // UI, das angezeigt wird, wenn Batterie leer
    public TimerEndScreenAndSceneSwitch timerScript; // Referenz auf Timer-Script, um Timer zu stoppen

    public string sceneToLoad = "";            // Name der Szene, die geladen werden soll (leer = nächste)
    public bool switchSceneAfterUI = false;    // Szene nach UI wechseln
    public float sceneSwitchDelay = 3f;        // Wartezeit bis Szenenwechsel

    public AudioClip warningSound;             // Warnsound
    public float soundVolume = 1f;

    private bool triggered = false;
    private AudioSource audioSource;

    void Start()
    {
        GameObject progressBarGO = GameObject.Find("Progress Bar");
        if (progressBarGO != null)
        {
            socialBatterie = progressBarGO.GetComponent<SocialBatterie>();
            if (socialBatterie == null)
                Debug.LogError("SocialBatterie-Komponente auf 'Progress Bar' nicht gefunden!");
        }
        else
        {
            Debug.LogError("GameObject 'Progress Bar' nicht gefunden!");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (lowEnergyScreen != null)
            lowEnergyScreen.SetActive(false);
    }

    void Update()
    {
        if (socialBatterie == null || lowEnergyScreen == null)
            return;

        if (socialBatterie.current <= socialBatterie.minimum && !triggered)
        {
            triggered = true;
            lowEnergyScreen.SetActive(true);
            Debug.Log("Batterie Minimum erreicht - UI aktiviert.");
            PlayWarningSound();

            if (timerScript != null)
                timerScript.StopTimer();

            if (switchSceneAfterUI)
                StartCoroutine(LoadSceneAfterDelay());
        }
    }

    void PlayWarningSound()
    {
        if (warningSound != null && audioSource != null)
        {
            audioSource.clip = warningSound;
            audioSource.volume = soundVolume;
            audioSource.Play();
        }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(sceneSwitchDelay);

        if (socialBatterie != null)
        {
            socialBatterie.current = 100; // Batterie wieder voll machen
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
            {
                Debug.Log("Szene wird geladen: " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("Szene '" + sceneToLoad + "' nicht im Build.");
            }
        }
        else
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int nextIndex = (currentIndex + 1) % SceneManager.sceneCountInBuildSettings;
            Debug.Log("Szene wird geladen: Index " + nextIndex);
            SceneManager.LoadScene(nextIndex);
        }
    }
}

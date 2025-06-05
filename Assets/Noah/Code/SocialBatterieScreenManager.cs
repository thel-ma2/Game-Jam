using UnityEngine;
using UnityEngine.SceneManagement;

public class SocialBatterieMinimumScreen : MonoBehaviour
{
    public SocialBatterie socialBatterie;      // Verweis auf dein SocialBatterie-Script
    public GameObject lowEnergyScreen;         // UI-Element, das angezeigt werden soll
    public string sceneToLoad = "";            // Name der Szene, die geladen werden soll
    public bool switchSceneAfterUI = false;    // Ob Szene nach UI angezeigt werden soll
    public float sceneSwitchDelay = 3f;        // Sekunden bis Szenenwechsel

    [Header("Soundeffekt")]
    public AudioClip warningSound;             // Der Sound, der abgespielt werden soll
    public float soundVolume = 1f;             // Lautstärke des Sounds

    private bool triggered = false;
    private AudioSource audioSource;

    void Start()
    {
        // Stelle sicher, dass AudioSource vorhanden ist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (socialBatterie == null || lowEnergyScreen == null)
            return;

        if (socialBatterie.current <= socialBatterie.minimum && !triggered)
        {
            triggered = true;
            lowEnergyScreen.SetActive(true);
            Debug.Log("Minimum erreicht – UI aktiviert.");
            PlayWarningSound();

            if (switchSceneAfterUI && !string.IsNullOrEmpty(sceneToLoad))
            {
                StartCoroutine(LoadSceneAfterDelay());
            }
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
        else
        {
            Debug.LogWarning("Warnsound oder AudioSource fehlt!");
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(sceneSwitchDelay);
        Debug.Log("Szene wird geladen: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}

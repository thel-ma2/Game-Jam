using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SocialBatterieMinimumScreen : MonoBehaviour
{
    public SocialBatterie socialBatterie;      // Verweis auf dein SocialBatterie-Script
    public GameObject lowEnergyScreen;         // UI-Element, das angezeigt werden soll
    public string sceneToLoad = "";            // Name der Szene, die geladen werden soll (leer = nächste Szene)
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

            if (switchSceneAfterUI)
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

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(sceneSwitchDelay);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Szene wird geladen: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Nächste Szene im Build laden
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
            Debug.Log("Szene wird geladen: Index " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

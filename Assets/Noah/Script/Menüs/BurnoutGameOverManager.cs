using UnityEngine;

public class BurnoutGameOverManager : MonoBehaviour
{
    public BurnoutScale burnoutScale;       // Referenz auf dein BurnoutScale-Script
    public GameObject gameOverCanvas;       // Canvas, das bei Game Over aktiviert wird
    public AudioClip gameOverSound;         // Der Sound, der abgespielt werden soll
    public float soundVolume = 1f;          // Lautstärke des Sounds

    private bool gameOverShown = false;     // Damit das Canvas nur einmal aktiviert wird
    private AudioSource audioSource;        // Zum Abspielen des Sounds

    void Start()
    {
        // AudioSource vorbereiten (entweder existierend oder automatisch hinzufügen)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (burnoutScale == null || gameOverCanvas == null)
            return;

        // Wenn Burnout auf Minimum sinkt und Game Over noch nicht gezeigt wurde
        if (burnoutScale.current <= burnoutScale.minimum && !gameOverShown)
        {
            gameOverCanvas.SetActive(true);
            PlayGameOverSound();
            gameOverShown = true;
        }
    }

    void PlayGameOverSound()
    {
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.clip = gameOverSound;
            audioSource.volume = soundVolume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Kein Game Over Sound oder AudioSource vorhanden!");
        }
    }
}

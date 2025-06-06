using UnityEngine;

public class SceneIntroPause : MonoBehaviour
{
    [Header("UI")]
    public GameObject introImage;

    [Header("Sound")]
    public AudioClip introSound;
    public float soundVolume = 1f;

    private AudioSource audioSource;
    private bool isWaitingForClick = true;

    void Start()
    {
        // UI aktivieren
        if (introImage != null)
            introImage.SetActive(true);

        // Zeit anhalten
        Time.timeScale = 0f;
        isWaitingForClick = true;

        // Audio vorbereiten
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.volume = soundVolume;
        audioSource.ignoreListenerPause = true; // Damit Sound trotz Pause hörbar ist
    }

    void Update()
    {
        if (isWaitingForClick && Input.GetMouseButtonDown(0)) // Linksklick
        {
            ResumeScene();
        }
    }

    void ResumeScene()
    {
        if (introImage != null)
            introImage.SetActive(false);

        Time.timeScale = 1f; // Spiel fortsetzen
        isWaitingForClick = false;

        // Sound erst jetzt abspielen
        if (introSound != null)
            audioSource.PlayOneShot(introSound);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerEndScreenAndSceneSwitch : MonoBehaviour
{
    [Header("Timer-Einstellungen")]
    public float timerDuration = 10f;             // Dauer des Timers
    private float timer = 0f;
    private bool timerRunning = true;

    [Header("Pointer-Einstellungen")]
    public RectTransform pointer;
    public float minimumX = 0f;
    public float maximumX = 200f;
    public Color pointerColor = Color.white;

    [Header("UI bei Ende")]
    public GameObject endScreenUI;                // Aktiviertes UI bei Ende
    public float timeBeforeSceneSwitch = 5f;      // Sekunden warten bis Szenenwechsel
    public string nextSceneName;                  // Name der Zielszene

    [Header("Soundeffekt")]
    public AudioSource audioSource;               // AudioSource für Soundeffekt
    public AudioClip warningClip;                 // Soundclip, der ab 80% gespielt wird

    private bool hasEnded = false;
    private bool soundPlayed = false;             // Damit der Sound nur einmal gespielt wird

    void Start()
    {
        timer = 0f;
        timerRunning = true;

        if (pointer != null)
        {
            Image img = pointer.GetComponent<Image>();
            if (img != null)
                img.color = pointerColor;
        }

        if (endScreenUI != null)
            endScreenUI.SetActive(false); // UI am Anfang deaktivieren

        if (audioSource == null && warningClip != null)
        {
            // Falls keine AudioSource zugewiesen, eine hinzufügen
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (!timerRunning || hasEnded)
            return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / timerDuration);

        // Sound abspielen, wenn 80% erreicht sind und noch nicht gespielt wurde
        if (!soundPlayed && t >= 0.8f && warningClip != null && audioSource != null)
        {
            audioSource.clip = warningClip;
            audioSource.Play();
            soundPlayed = true;
        }

        // Pointer-Position updaten
        if (pointer != null)
        {
            float newX = Mathf.Lerp(minimumX, maximumX, t);
            Vector2 pos = pointer.anchoredPosition;
            pos.x = newX;
            pointer.anchoredPosition = pos;
        }

        // Timer erreicht Maximum
        if (timer >= timerDuration)
        {
            timerRunning = false;
            hasEnded = true;

            Debug.Log("Timer erreicht Maximum.");

            if (endScreenUI != null)
                endScreenUI.SetActive(true);

            if (!string.IsNullOrEmpty(nextSceneName))
                Invoke(nameof(LoadNextScene), timeBeforeSceneSwitch);
        }
    }

    void LoadNextScene()
    {
        Debug.Log("Wechsle zu Szene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}

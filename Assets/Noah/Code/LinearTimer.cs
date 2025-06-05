using UnityEngine;
using UnityEngine.UI;

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
    public float delayBeforeAdditionalUI = 2f;    // Sekunden warten bis weiteres UI erscheint
    public GameObject additionalCanvasUI;         // Der zusätzliche Canvas

    [Header("Soundeffekt")]
    public AudioSource audioSource;               // AudioSource für Soundeffekt
    public AudioClip warningClip;                 // Soundclip, der ab 80% gespielt wird

    private bool hasEnded = false;
    private bool soundPlayed = false;

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

        if (additionalCanvasUI != null)
            additionalCanvasUI.SetActive(false); // Zweites UI auch deaktivieren

        if (audioSource == null && warningClip != null)
        {
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

        if (!soundPlayed && t >= 0.8f && warningClip != null && audioSource != null)
        {
            audioSource.clip = warningClip;
            audioSource.Play();
            soundPlayed = true;
        }

        if (pointer != null)
        {
            float newX = Mathf.Lerp(minimumX, maximumX, t);
            Vector2 pos = pointer.anchoredPosition;
            pos.x = newX;
            pointer.anchoredPosition = pos;
        }

        if (timer >= timerDuration)
        {
            timerRunning = false;
            hasEnded = true;

            Debug.Log("Timer erreicht Maximum.");

            if (endScreenUI != null)
                endScreenUI.SetActive(true);

            // Nach einer Verzögerung weiteres UI aktivieren
            if (additionalCanvasUI != null)
                Invoke(nameof(ActivateAdditionalUI), delayBeforeAdditionalUI);
        }
    }

    void ActivateAdditionalUI()
    {
        Debug.Log("Aktiviere zusätzliches UI.");
        additionalCanvasUI.SetActive(true);
    }
}

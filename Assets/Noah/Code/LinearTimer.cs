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

    private bool hasEnded = false;

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
    }

    void Update()
    {
        if (!timerRunning || hasEnded)
            return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / timerDuration);

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

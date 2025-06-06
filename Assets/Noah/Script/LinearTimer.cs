using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LinearTimer : MonoBehaviour
{
    public float timerDuration = 10f;
    public RectTransform pointer;
    public float minimumX = 0f;
    public float maximumX = 200f;
    public Color pointerColor = Color.white;

    public GameObject endScreenUI;
    public float delayBeforeSceneSwitch = 2f;

    private float timer = 0f;
    private bool timerRunning = true;

    [Header("Abhängigkeiten")]
    public Quest quest;                     // Quest, die überprüft werden soll
    private BurnoutScale burnoutScale;     // Burnout-Meter Referenz

    [Header("BurnoutMeter Prefab")]
    public GameObject burnoutMeterPrefab;  // Prefab wird im Inspector gesetzt

    public int burnoutDecreaseAmount = 1;  // Burnout verringern um 1, wenn Quest nicht abgeschlossen

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
            endScreenUI.SetActive(false);

        // Versuche BurnoutScale zu finden
        burnoutScale = FindObjectOfType<BurnoutScale>();

        // Wenn nicht gefunden, Prefab instanziieren
        if (burnoutScale == null)
        {
            if (burnoutMeterPrefab != null)
            {
                GameObject burnoutGO = Instantiate(burnoutMeterPrefab);
                burnoutScale = burnoutGO.GetComponent<BurnoutScale>();

                if (burnoutScale == null)
                {
                    Debug.LogWarning("Instanziiertes Prefab hat keine BurnoutScale-Komponente!");
                }
                else
                {
                    Debug.Log("BurnoutMeter Prefab instanziiert.");
                }
            }
            else
            {
                Debug.LogWarning("Kein BurnoutMeter Prefab gesetzt und kein BurnoutScale in der Szene gefunden.");
            }
        }
    }

    void Update()
    {
        if (!timerRunning)
            return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / timerDuration);

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
            HandleQuestOutcome();

            if (endScreenUI != null)
                endScreenUI.SetActive(true);

            Invoke(nameof(SwitchScene), delayBeforeSceneSwitch);
        }
    }

    void HandleQuestOutcome()
    {
        if (quest != null && burnoutScale != null)
        {
            bool questCompleted = quest.foundItems >= quest.requiredItems;

            if (!questCompleted)
            {
                burnoutScale.current = Mathf.Max(
                    burnoutScale.minimum,
                    burnoutScale.current - burnoutDecreaseAmount
                );
                Debug.Log("Quest nicht abgeschlossen → Burnout wird verringert.");
            }
            else
            {
                Debug.Log("Quest abgeschlossen → Kein Burnout-Abzug.");
            }
        }
        else
        {
            Debug.LogWarning("Referenz zu Quest oder BurnoutScale fehlt!");
        }
    }

    void SwitchScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            Debug.LogWarning("Keine weitere Szene im Build.");
    }

    public void StopTimer()
    {
        timerRunning = false;
        Debug.Log("Timer wurde gestoppt.");
    }
}

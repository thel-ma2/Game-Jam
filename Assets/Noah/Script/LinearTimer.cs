using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerEndScreenAndSceneSwitch : MonoBehaviour
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
            if (endScreenUI != null)
                endScreenUI.SetActive(true);

            Invoke(nameof(SwitchScene), delayBeforeSceneSwitch);
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
        Debug.Log("Timer wurde gestoppt (Batterie Minimum erreicht).");
    }

    void SwitchScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            Debug.LogWarning("Keine weitere Szene im Build. Szenenwechsel abgebrochen.");
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerEndScreenAndSceneSwitch : MonoBehaviour
{
    public float timerDuration = 10f;
    private float timer = 0f;
    private bool timerRunning = true;

    public RectTransform pointer;
    public float minimumX = 0f;
    public float maximumX = 200f;
    public Color pointerColor = Color.white;

    public GameObject endScreenUI;
    public float delayBeforeSceneSwitch = 2f;

    public AudioSource audioSource;
    public AudioClip warningClip;

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
            endScreenUI.SetActive(false);

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

            if (endScreenUI != null)
                endScreenUI.SetActive(true);

            Invoke(nameof(SwitchScene), delayBeforeSceneSwitch);
        }
    }

    void SwitchScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Keine weitere Szene im Build-Settings. Szene nicht gewechselt.");
        }
    }
}

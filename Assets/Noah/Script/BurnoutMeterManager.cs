using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quest;

public class BurnoutMeterManager : MonoBehaviour
{
    public BurnoutScale burnoutScale;
    public Image pointerImage;
    public Color flashColor = Color.red;
    public float flashDuration = 0.2f;

    public AudioSource audioSource;
    public AudioClip burnoutIncreaseSound;

    [Tooltip("Alle QuestSystem-ScriptableObjects, die den Burnout beeinflussen.")]
    public List<QuestSystem> relevantQuests = new List<QuestSystem>();

    private Color originalColor;

    private void Start()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.AddListener<BeforeSceneChangeEvent>(OnBeforeSceneChange);
            EventManager.Instance.AddListener<SceneChangedEvent>(OnSceneChanged);
        }

        if (pointerImage != null)
        {
            originalColor = pointerImage.color;
        }
    }

    private void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.RemoveListener<BeforeSceneChangeEvent>(OnBeforeSceneChange);
            EventManager.Instance.RemoveListener<SceneChangedEvent>(OnSceneChanged);
        }
    }

    private void OnBeforeSceneChange(BeforeSceneChangeEvent eventData)
    {
        Debug.Log($"BeforeSceneChangeEvent erhalten. Zielszene: {eventData.NextSceneName}");
        PrüfeUndErhöheBurnoutWert();
    }

    private void OnSceneChanged(SceneChangedEvent e)
    {
        Debug.Log("SceneChangedEvent empfangen");
        // Falls du hier noch was machen willst nach Szenenwechsel, ansonsten leer lassen
    }

    public void PrüfeUndErhöheBurnoutWert()
    {
        if (burnoutScale == null)
        {
            Debug.LogWarning("BurnoutScale ist nicht gesetzt.");
            return;
        }

        if (relevantQuests == null || relevantQuests.Count == 0)
        {
            Debug.LogWarning("Keine relevanten Quests gesetzt.");
            return;
        }

        int offeneRelevanteQuests = 0;

        foreach (QuestSystem quest in relevantQuests)
        {
            Debug.Log($"Prüfe Quest: {quest.name}, affectsBurnoutMeter: {quest.affectsBurnoutMeter}, Completed: {quest.Completed}");

            if (quest.affectsBurnoutMeter && !quest.Completed)
            {
                offeneRelevanteQuests++;
            }
        }

        Debug.Log($"Anzahl offene relevante Quests: {offeneRelevanteQuests}");

        if (offeneRelevanteQuests > 0)
        {
            burnoutScale.current = Mathf.Clamp(
                burnoutScale.current + offeneRelevanteQuests,
                burnoutScale.minimum,
                burnoutScale.maximum
            );

            Debug.Log($"Burnout erhöht um {offeneRelevanteQuests} Punkte.");

            if (pointerImage != null)
                StartCoroutine(FlashPointer());

            if (audioSource != null && burnoutIncreaseSound != null)
                audioSource.PlayOneShot(burnoutIncreaseSound);
        }
        else
        {
            Debug.Log("Keine offenen relevanten Quests – kein Burnout-Anstieg.");
        }
    }

    private IEnumerator FlashPointer()
    {
        pointerImage.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        pointerImage.color = originalColor;
    }
}

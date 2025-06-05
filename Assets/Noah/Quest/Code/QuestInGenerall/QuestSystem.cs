using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Quest;

[CreateAssetMenu(menuName = "Quest System/Quest")]
public class QuestSystem : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public string Name;

        [TextArea]
        public string Description;
    }

    [System.Serializable]
    public class QuestReward
    {
        public string rewardName;
        public int amount;
    }

    public Info Information;
    public List<QuestGoal> Goals;

    [Header("Belohnungen")]
    public List<QuestReward> Rewards = new List<QuestReward>();  // WICHTIG: Initialisierung hier!

    [Header("Burnout Meter Integration")]
    [Tooltip("Referenz auf das BurnoutScale-Objekt, das beeinflusst werden soll")]
    public BurnoutScale burnoutScale;

    [Tooltip("Wie stark der Burnout-Wert bei Questabschluss verändert wird (positiv oder negativ)")]
    public int burnoutValueChange = 0;

    public bool Completed { get; private set; }
    public UnityEvent QuestCompleted;

    [Header("Optionales UI-Feedback")]
    public GameObject questCompletionImage;

    [Header("Burnout Meter")]
    [Tooltip("Diese Quest beeinflusst das Burnout Meter, wenn sie nicht abgeschlossen ist.")]
    public bool affectsBurnoutMeter = false;

    private bool initialized = false;

    public void Initialize()
    {
        if (initialized) return;
        initialized = true;

        Completed = false;

        if (QuestCompleted == null)
            QuestCompleted = new UnityEvent();

        foreach (var goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(CheckGoals);
        }

        if (EventManager.Instance != null)
        {
            EventManager.Instance.AddListener<SceneChangedEvent>(OnSceneChanged);
        }

        CheckGoals();
    }

    private void OnSceneChanged(SceneChangedEvent e)
    {
        CheckGoals();
    }

    private void CheckGoals()
    {
        if (Completed) return;

        Completed = Goals.All(g => g.Completed);

        if (Completed)
        {
            GiveRewards();
            ApplyBurnoutChange();

            QuestCompleted?.Invoke();
            QuestCompleted.RemoveAllListeners();

            if (questCompletionImage != null)
                questCompletionImage.SetActive(true);
        }
    }

    private void GiveRewards()
    {
        if (Rewards == null || Rewards.Count == 0)
        {
            Debug.LogWarning("Keine Belohnungen definiert!");
            return;
        }

        foreach (var reward in Rewards)
        {
            Debug.Log($"Belohnung erhalten: {reward.rewardName} x{reward.amount}");
            // Hier kannst du deine Belohnungslogik ergänzen, z.B. Inventar erweitern oder XP geben
        }
    }

    private void ApplyBurnoutChange()
    {
        if (burnoutScale == null)
            return;

        int neuerWert = burnoutScale.current + burnoutValueChange;
        burnoutScale.current = Mathf.Clamp(neuerWert, burnoutScale.minimum, burnoutScale.maximum);

        Debug.Log($"BurnoutScale wurde um {burnoutValueChange} verändert. Neuer Wert: {burnoutScale.current}");
    }

    public string GetTitle() => Information.Name;
    public string GetDescription() => Information.Description;

    public void Cleanup()
    {
        foreach (var goal in Goals)
        {
            goal.GoalCompleted.RemoveListener(CheckGoals);
        }

        if (EventManager.Instance != null)
        {
            EventManager.Instance.RemoveListener<SceneChangedEvent>(OnSceneChanged);
        }
    }
}

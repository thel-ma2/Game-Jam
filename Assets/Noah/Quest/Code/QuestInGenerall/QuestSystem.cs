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

    public Info Information;
    public List<QuestGoal> Goals;
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
            QuestCompleted?.Invoke();
            QuestCompleted.RemoveAllListeners();

            if (questCompletionImage != null)
                questCompletionImage.SetActive(true);
        }
    }

    public string GetTitle() => Information.Name;
    public string GetDescription() => Information.Description;

    /// <summary>
    /// Entfernt alle Event-Listener, z.B. wenn die Quest entfernt wird.
    /// </summary>
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

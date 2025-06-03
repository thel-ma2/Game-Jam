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
    public GameObject questCompletionImage; // Bild, das bei Abschluss aktiviert wird

    public void Initialize()
    {
        Completed = false;
        if (QuestCompleted == null)
            QuestCompleted = new UnityEvent();

        foreach (var goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(CheckGoals);
        }
    }

    private void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        if (Completed)
        {
            QuestCompleted?.Invoke();
            QuestCompleted.RemoveAllListeners();

            if (questCompletionImage != null)
                questCompletionImage.SetActive(true); // UI-Bild anzeigen
        }
    }

    public string GetTitle() => Information.Name;
    public string GetDescription() => Information.Description;
}

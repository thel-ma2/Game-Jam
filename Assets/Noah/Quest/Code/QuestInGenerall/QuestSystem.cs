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
        public string Name; // z. B. "Frühstück vorbereiten"

        [TextArea]
        public string Description; // z. B. "Sammle Toast, Eier und Milch."
    }

    public Info Information;

    public List<QuestGoal> Goals;
    public bool Completed { get; private set; }
    public UnityEvent QuestCompleted;

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
        }
    }

    // Diese Methoden werden vom UI Script benötigt
    public string GetTitle() => Information.Name;
    public string GetDescription() => Information.Description;
}

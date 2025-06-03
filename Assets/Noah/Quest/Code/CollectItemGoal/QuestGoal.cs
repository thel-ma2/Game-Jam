using UnityEngine;
using UnityEngine.Events;

namespace Quest
{
    public abstract class QuestGoal : ScriptableObject
    {
        public bool Completed { get; protected set; }
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;

        public UnityEvent GoalCompleted;

        // Methode muss 'virtual' sein, damit sie überschrieben werden kann
        public virtual string GetDescription()
        {
            return "Standard Quest-Zielbeschreibung";
        }

        public virtual void Initialize()
        {
            Completed = false;
            CurrentAmount = 0;
            if (GoalCompleted == null)
                GoalCompleted = new UnityEvent();
            else
                GoalCompleted.RemoveAllListeners();
        }

        protected void Evaluate()
        {
            if (CurrentAmount >= RequiredAmount && !Completed)
            {
                Complete();
            }
        }

        protected void Complete()
        {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace Quest
{
    [CreateAssetMenu(menuName = "Quest System/Goals/Collect Item Goal")]
    public class CollectItemGoal : QuestGoal
    {
        public string ItemName;
        public new int RequiredAmount = 1;
        private int currentAmount = 0;

        public void OnItemCollected(string collectedItemName)
        {
            if (collectedItemName == ItemName && !Completed)
            {
                currentAmount++;
                Evaluate();
            }
        }

        private new void Evaluate()
        {
            if (currentAmount >= RequiredAmount)
            {
                Completed = true;
                GoalCompleted?.Invoke();
                Debug.Log($"Sammelziel für {ItemName} abgeschlossen!");
            }
        }
    }
}

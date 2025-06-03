using UnityEngine;
using Quest;

namespace Quest
{
    [CreateAssetMenu(menuName = "Quest System/Goals/Build Goal")]
    public class BuildingGoal : QuestGoal
    {
        [Header("Building Name")]
        public string Building; // Der Name des Gebäudes, das gebaut werden muss

        // Überschreibt die Beschreibung für dieses Ziel
        public override string GetDescription()
        {
            return $"Baue ein {Building}";
        }

        public override void Initialize()
        {
            base.Initialize();

            // Beispiel: EventManager lauscht auf Bau-Events (muss im Projekt existieren)
            EventManager.Instance.AddListener<BuildingGameEvent>(OnBuilding);
        }

        private void OnBuilding(BuildingGameEvent eventInfo)
        {
            if (eventInfo.BuildingName == Building)
            {
                CurrentAmount++;
                Evaluate();
            }
        }
    }
}

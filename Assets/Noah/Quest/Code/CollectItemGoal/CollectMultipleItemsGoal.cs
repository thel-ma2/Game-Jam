using System.Collections.Generic;
using System.Linq;
using Quest;
using UnityEngine;

// QuestGoal, bei dem mehrere Fr�hst�cksitems gesammelt werden m�ssen
[CreateAssetMenu(menuName = "Quest System/Goals/Collect Multiple Breakfast Items")]
public class CollectMultipleItemsGoal : QuestGoal
{
    // Welche Items und wie viele davon gesammelt werden m�ssen (im Inspector eintragen)
    public List<BreakfastItem> RequiredItems = new List<BreakfastItem>();
    public List<int> RequiredAmounts = new List<int>();

    // Interne Dictionaries f�r schnelle Abfragen
    private Dictionary<BreakfastItem, int> requiredDict = new Dictionary<BreakfastItem, int>();
    private Dictionary<BreakfastItem, int> currentAmounts = new Dictionary<BreakfastItem, int>();

    public override void Initialize()
    {
        base.Initialize();

        requiredDict.Clear();
        currentAmounts.Clear();

        // Dict f�llen, Annahme: RequiredItems & RequiredAmounts haben gleiche L�nge
        for (int i = 0; i < RequiredItems.Count; i++)
        {
            var item = RequiredItems[i];
            var amount = (i < RequiredAmounts.Count) ? RequiredAmounts[i] : 1;

            requiredDict[item] = amount;
            currentAmounts[item] = 0;
        }
    }

    // Wird vom Inventory aufgerufen, wenn ein Item eingesammelt wurde
    public void OnItemCollected(BreakfastItem collectedItem)
    {
        if (requiredDict.ContainsKey(collectedItem))
        {
            currentAmounts[collectedItem]++;
            Evaluate();
        }
    }

    // Versteckt die Evaluate-Methode der Basisklasse, um Warnungen zu vermeiden
    protected new void Evaluate()
    {
        Completed = requiredDict.All(kvp => currentAmounts.ContainsKey(kvp.Key) && currentAmounts[kvp.Key] >= kvp.Value);

        if (Completed)
        {
            Complete(); // QuestGoal als abgeschlossen markieren
        }
    }
}

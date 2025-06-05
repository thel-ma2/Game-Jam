using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Quest;

[CreateAssetMenu(menuName = "Quest System/Goals/Collect Multiple Breakfast Items")]
public class CollectMultipleItemsGoal : QuestGoal
{
    public string QuestName = "Neue Quest";

    [Tooltip("Wenn aktiviert, müssen mehrere verschiedene Items gesammelt werden. Andernfalls wird nur das erste Item in der Liste verwendet.")]
    public bool UseMultipleItemTypes = true;

    public List<BreakfastItem> RequiredItems = new List<BreakfastItem>();
    public List<int> RequiredAmounts = new List<int>();

    public GameObject InteractionObject;

    private Dictionary<BreakfastItem, int> requiredDict = new Dictionary<BreakfastItem, int>();
    private Dictionary<BreakfastItem, int> currentAmounts = new Dictionary<BreakfastItem, int>();

    private bool allItemsCollected = false;
    private bool interactionDone = false;

    public bool InteractionRequired => InteractionObject != null;

    public override void Initialize()
    {
        base.Initialize();

        requiredDict.Clear();
        currentAmounts.Clear();

        // Falls UseMultipleItemTypes false ist, nur das erste Item aus der Liste verwenden
        if (!UseMultipleItemTypes && RequiredItems.Count > 0)
        {
            var item = RequiredItems[0];
            int amount = (RequiredAmounts.Count > 0) ? RequiredAmounts[0] : 1;

            requiredDict[item] = amount;
            currentAmounts[item] = 0;
        }
        else
        {
            for (int i = 0; i < RequiredItems.Count; i++)
            {
                var item = RequiredItems[i];
                var amount = (i < RequiredAmounts.Count) ? RequiredAmounts[i] : 1;

                requiredDict[item] = amount;
                currentAmounts[item] = 0;
            }
        }

        allItemsCollected = false;
        interactionDone = false;
    }

    public void OnItemCollected(BreakfastItem collectedItem)
    {
        if (requiredDict.ContainsKey(collectedItem))
        {
            currentAmounts[collectedItem]++;
            EvaluateCollection();
        }
    }

    private void EvaluateCollection()
    {
        allItemsCollected = requiredDict.All(kvp =>
            currentAmounts.ContainsKey(kvp.Key) &&
            currentAmounts[kvp.Key] >= kvp.Value);
    }

    public void OnInteractionDone()
    {
        if (allItemsCollected && InteractionRequired && !interactionDone)
        {
            interactionDone = true;
            Complete();
        }
    }

    public int GetCurrentAmount(BreakfastItem item)
    {
        if (currentAmounts.ContainsKey(item))
            return currentAmounts[item];
        return 0;
    }

    public int GetRequiredAmount(BreakfastItem item)
    {
        if (requiredDict.ContainsKey(item))
            return requiredDict[item];
        return 0;
    }

    public bool IsInteractionDone() => interactionDone;

    public bool AreAllItemsCollected() => allItemsCollected;

    public List<BreakfastItem> GetActiveItems()
    {
        return requiredDict.Keys.ToList();
    }
}

using System.Collections.Generic;
using UnityEngine;
using Quest;

public class PlayerInventory : MonoBehaviour
{
    public List<QuestSystem> ActiveQuests = new List<QuestSystem>(); // Liste aktiver Quests

    public void CollectItem(BreakfastItem item)
    {
        Debug.Log($"Item eingesammelt: {item.ItemName}");

        foreach (var quest in ActiveQuests)
        {
            foreach (var goal in quest.Goals)
            {
                if (goal is CollectMultipleItemsGoal multiGoal)
                {
                    multiGoal.OnItemCollected(item);
                }
                else if (goal is CollectItemGoal singleGoal)
                {
                    singleGoal.OnItemCollected(item.ItemName);
                }
            }
        }
    }
}

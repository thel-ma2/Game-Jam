using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScriptGoal : MonoBehaviour
{
    [Header("Zuweisung des Goals")]
    [SerializeField] private CollectMultipleItemsGoal questGoal; // <-- Goal direkt zuweisbar im Inspector

    [Header("UI Referenzen")]
    public TextMeshProUGUI questNameText;
    public Transform itemListContainer;
    public TextMeshProUGUI interactionRequiredText;

    [Header("Itemzeilen (Template)")]
    public TextMeshProUGUI itemTextPrefab; // deaktivierte Text-Vorlage

    private List<TextMeshProUGUI> itemTexts = new();
    private List<BreakfastItem> items = new();

    private void Start()
    {
        if (questGoal != null)
        {
            questGoal.Initialize();       // Ziel initialisieren
            Initialize(questGoal);        // UI aufbauen
        }
        else
        {
            Debug.LogWarning("Kein QuestGoal im UIScriptGoal zugewiesen.");
        }
    }

    public void Initialize(CollectMultipleItemsGoal goal)
    {
        questGoal = goal;

        if (questNameText != null)
            questNameText.text = goal.QuestName;

        foreach (Transform child in itemListContainer)
            Destroy(child.gameObject);

        itemTexts.Clear();
        items = goal.GetActiveItems();

        foreach (var item in items)
        {
            var textInstance = Instantiate(itemTextPrefab, itemListContainer);
            textInstance.gameObject.SetActive(true);

            int current = goal.GetCurrentAmount(item);
            int required = goal.GetRequiredAmount(item);
            textInstance.text = $"{item.ItemName}: {current} / {required}";

            itemTexts.Add(textInstance);
        }

        if (interactionRequiredText != null)
        {
            interactionRequiredText.gameObject.SetActive(goal.InteractionRequired);
            interactionRequiredText.text = goal.InteractionRequired
                ? "Bitte mit dem Objekt interagieren."
                : "";
        }
    }

    private void Update()
    {
        if (questGoal == null) return;

        for (int i = 0; i < items.Count && i < itemTexts.Count; i++)
        {
            var item = items[i];
            int current = questGoal.GetCurrentAmount(item);
            int required = questGoal.GetRequiredAmount(item);
            itemTexts[i].text = $"{item.ItemName}: {current} / {required}";
        }

        if (questGoal.InteractionRequired && interactionRequiredText != null)
        {
            interactionRequiredText.text = questGoal.IsInteractionDone()
                ? "Interaktion abgeschlossen."
                : "Bitte mit dem Objekt interagieren.";
        }
    }
}

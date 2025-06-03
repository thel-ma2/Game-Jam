using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestListUIManager : MonoBehaviour
{
    public Transform contentParent; // Parent für Einträge (z.B. Content-Objekt im ScrollView)
    public QuestListEntry questEntryPrefab;

    // Liste aller aktiven Quests
    private List<QuestSystem> activeQuests = new List<QuestSystem>();
    private List<QuestListEntry> displayedEntries = new List<QuestListEntry>();

    void Start()
    {
        // Beispiel: Quests laden (kann angepasst werden)
        LoadActiveQuests();

        // Erstmal Quests anzeigen (standardmäßig sortiert nach Name)
        DisplayQuests();
    }

    public void LoadActiveQuests()
    {
        // Hier solltest du deine aktive Quest-Liste füllen,
        // z.B. aus einem QuestManager oder Ressourcen-Ordner.
        // Für Demo fügen wir einfach leere Liste ein:

        // activeQuests = DeineQuests;
    }

    public void DisplayQuests()
    {
        ClearEntries();

        foreach (var quest in activeQuests)
        {
            var entry = Instantiate(questEntryPrefab, contentParent);
            entry.Setup(quest.GetTitle(), quest.GetDescription());
            displayedEntries.Add(entry);
        }
    }

    public void ClearEntries()
    {
        foreach (var entry in displayedEntries)
        {
            Destroy(entry.gameObject);
        }
        displayedEntries.Clear();
    }

    // Sortiert Quests alphabetisch nach Name und zeigt neu an
    public void SortByName()
    {
        activeQuests = activeQuests.OrderBy(q => q.GetTitle()).ToList();
        DisplayQuests();
    }

    // Sortiert Quests nach Status (fertig zuerst)
    public void SortByCompletion()
    {
        activeQuests = activeQuests.OrderByDescending(q => q.Completed).ToList();
        DisplayQuests();
    }

    // Du kannst noch mehr Sortier-Methoden hinzufügen...
}

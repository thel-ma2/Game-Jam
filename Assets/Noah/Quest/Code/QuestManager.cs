using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private readonly List<QuestSystem> activeQuests = new();

    public delegate void QuestListChanged();
    public event QuestListChanged OnQuestListChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddQuest(QuestSystem quest)
    {
        if (quest == null)
        {
            Debug.LogWarning("[QuestManager] Versuch, eine NULL-Quest hinzuzufügen.");
            return;
        }

        if (activeQuests.Contains(quest))
        {
            Debug.LogWarning($"[QuestManager] Quest bereits aktiv: {quest.GetTitle()}");
            return;
        }

        quest.Initialize();
        activeQuests.Add(quest);
        Debug.Log($"[QuestManager] Quest gestartet: {quest.GetTitle()}");

        OnQuestListChanged?.Invoke();
    }

    public void RemoveQuest(QuestSystem quest)
    {
        if (quest == null) return;

        if (activeQuests.Remove(quest))
        {
            Debug.Log($"[QuestManager] Quest entfernt: {quest.GetTitle()}");
            OnQuestListChanged?.Invoke();
        }
    }

    public void CleanupCompletedQuests()
    {
        int removed = activeQuests.RemoveAll(q => q.Completed);
        if (removed > 0)
        {
            Debug.Log($"[QuestManager] {removed} abgeschlossene Quests entfernt.");
            OnQuestListChanged?.Invoke();
        }
    }

    public IReadOnlyList<QuestSystem> GetActiveQuests()
    {
        return activeQuests.AsReadOnly();
    }
}

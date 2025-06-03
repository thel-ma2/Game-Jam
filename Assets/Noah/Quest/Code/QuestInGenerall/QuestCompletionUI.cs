using UnityEngine;
using UnityEngine.UI;
using Quest;

public class QuestCompletionUI : MonoBehaviour
{
    [Header("Quest-Referenz")]
    public QuestSystem quest;

    [Header("Bild, das deaktiviert wird bei Abschluss")]
    public GameObject completionImage; // z. B. ein Haken-Icon

    void Start()
    {
        if (quest == null || completionImage == null)
        {
            Debug.LogWarning("Quest oder Bild nicht zugewiesen.");
            return;
        }

        // Sicherstellen, dass das Bild erstmal AN ist
        completionImage.SetActive(true);

        // Event abonnieren
        quest.QuestCompleted.AddListener(OnQuestCompleted);

        // Falls die Quest schon abgeschlossen ist (z. B. bei Szenenwechsel), sofort ausblenden
        if (quest.Completed)
        {
            completionImage.SetActive(false);
        }
    }

    private void OnQuestCompleted()
    {
        completionImage.SetActive(false);
    }
}

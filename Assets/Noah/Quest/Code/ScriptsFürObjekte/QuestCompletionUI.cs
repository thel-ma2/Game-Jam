using UnityEngine;
using UnityEngine.UI;
using Quest;

public class QuestCompletionUI : MonoBehaviour
{
    [Header("Quest-Referenz")]
    public QuestSystem quest;

    [Header("Abzuschaltendes Objekt bei Abschluss")]
    public GameObject completionImage;

    void Start()
    {
        if (quest == null || completionImage == null)
            return;

        completionImage.SetActive(true);
        quest.QuestCompleted.AddListener(OnQuestCompleted);

        if (quest.Completed)
            completionImage.SetActive(false);
    }

    private void OnQuestCompleted()
    {
        completionImage.SetActive(false);
    }
}

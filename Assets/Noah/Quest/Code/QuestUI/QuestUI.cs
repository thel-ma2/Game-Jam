using UnityEngine;
using TMPro; // Wichtig für TextMeshPro

public class QuestUI_TMP : MonoBehaviour
{
    public QuestSystem ActiveQuest; // Referenz zur aktiven Quest

    public TMP_Text questNameText;         // z. B. "Frühstücksmission"
    public TMP_Text questDescriptionText;  // z. B. "Sammle Toast, Milch und Eier."

    void Start()
    {
        if (ActiveQuest != null)
        {
            questNameText.text = ActiveQuest.Information.Name;
            questDescriptionText.text = ActiveQuest.Information.Description;
        }
    }
}

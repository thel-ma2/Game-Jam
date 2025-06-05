using UnityEngine;
using TMPro;
using System.Text;  // Für StringBuilder
using Quest;        // Dein Quest Namespace

public class QuestUI_TMP : MonoBehaviour
{
    public QuestSystem ActiveQuest;

    public TMP_Text questNameText;
    public TMP_Text questDescriptionText; // Hier steht die Liste der noch fehlenden Items

    void Start()
    {
        UpdateQuestUI();
    }

    void Update()
    {
        UpdateQuestUI();
    }

    void UpdateQuestUI()
    {
        if (ActiveQuest != null)
        {
            // Questname anzeigen
            questNameText.text = ActiveQuest.Information.Name;

            StringBuilder sb = new StringBuilder();
            bool allGoalsCompleted = true;

            foreach (var goal in ActiveQuest.Goals)
            {
                if (goal is CollectMultipleItemsGoal collectGoal)
                {
                    bool anyItemLeft = false;

                    for (int i = 0; i < collectGoal.RequiredItems.Count; i++)
                    {
                        var item = collectGoal.RequiredItems[i];
                        int current = collectGoal.GetCurrentAmount(item);
                        int required = collectGoal.GetRequiredAmount(item);

                        // Zeige nur Items an, die noch fehlen
                        if (current < required)
                        {
                            sb.AppendLine($"{item.ItemName}: {current} / {required}");
                            anyItemLeft = true;
                        }
                    }

                    if (anyItemLeft)
                    {
                        sb.AppendLine("\nSammle die fehlenden Items...");
                        allGoalsCompleted = false;
                    }
                    else
                    {
                        if (!collectGoal.IsInteractionDone())
                        {
                            sb.AppendLine("\nInteragiere mit dem Objekt, um die Quest abzuschließen.");
                            allGoalsCompleted = false;
                        }
                    }
                }
                else
                {
                    // Andere Goal-Typen hier NICHT anzeigen (keine statische Description mehr)
                    allGoalsCompleted = allGoalsCompleted && goal.Completed;
                }
            }

            if (allGoalsCompleted)
            {
                sb.AppendLine("\n<color=green>Quest abgeschlossen!</color>");
            }

            questDescriptionText.text = sb.ToString();
        }
        else
        {
            // Wenn keine Quest aktiv, alles leer
            questNameText.text = "";
            questDescriptionText.text = "";
        }
    }
}

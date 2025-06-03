using TMPro;
using UnityEngine;

public class QuestListEntry : MonoBehaviour
{
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questDescriptionText;

    public void Setup(string name, string description)
    {
        questNameText.text = name;
        questDescriptionText.text = description;
    }
}

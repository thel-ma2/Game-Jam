using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int requiredItems = 1;
    public int foundItems = 0;

    public TextMeshProUGUI itemsCollected;

    private void Start()
    {
        foundItems = 0;
    }

    // Update is called once per frame
    public void ItemsCollected()
    {
        foundItems += 1;

        itemsCollected.text = "Items collected" + foundItems + "/" + requiredItems;

        Debug.Log("Collected 1");
    }
}

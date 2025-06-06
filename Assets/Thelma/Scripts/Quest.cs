using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int requiredItems = 1;
    public int foundItems = 0;

    public TextMeshProUGUI itemsCollected;
    public GameObject toDoKreuz;

    private AudioSource audioSource;
    [SerializeField] private AudioClip questComplete;

    private void Start()
    {
        foundItems = 0;
        toDoKreuz.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }
    public void ItemsCollected()
    {
        foundItems += 1;

        itemsCollected.text = "Items collected      " + foundItems + "/" + requiredItems;
        Debug.Log("Collected " + foundItems);



        if (foundItems == requiredItems)
        {
            toDoKreuz.gameObject.SetActive(true);
            audioSource.PlayOneShot(questComplete);
        }
        else
            return;
    }
    
}

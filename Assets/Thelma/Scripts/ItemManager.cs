using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [Header("Manager")]
    public bool isFound = false;
    public int amount = 0;
    public GameObject quest;

    [Header("Items Lists")]
    public List<GameObject> homeItems;
    public List<GameObject> libraryItems;
    public List<GameObject> shoptems;
    public List<GameObject> storageItems;


    private AudioSource audioSource;
    [SerializeField] private AudioClip itemsAufsammeln;

    private void Start()
    {
        amount = 0;
        audioSource = GetComponent<AudioSource>();
    }

    public void ItemFound()
    {
        amount += 1;
        audioSource.PlayOneShot(itemsAufsammeln);
        Debug.Log("Item found " + amount);

        quest.GetComponent<Quest>().ItemsCollected();
    }
}

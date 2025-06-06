using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.SearchService;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [Header("Manager")]
    public bool isFound = false;
    public int amount = 0;
    private GameObject kochen;
    private GameObject aufraumen;
    private GameObject arbeit;
    private GameObject bucher;
    private GameObject supermarkt;

    [Header("Items Lists")]
    public List<GameObject> homeItems;
    public List<GameObject> libraryItems;
    public List<GameObject> shoptems;
    public List<GameObject> storageItems;


    private AudioSource audioSource;
    [SerializeField] private AudioClip itemsAufsammeln;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        kochen = GameObject.FindWithTag("WGQuestKochen");
        aufraumen = GameObject.FindWithTag("WGQuestAufräumen");
        arbeit = GameObject.FindWithTag("LagerQuestArbeit");
        bucher = GameObject.FindWithTag("BibQuestBücher");
        supermarkt = GameObject.FindWithTag("SupermarktQuest");
    }

    public void ItemFound()
    {
        amount += 1;
        audioSource.PlayOneShot(itemsAufsammeln);
        Debug.Log("Item found " + amount);

        kochen.GetComponent<Quest>().ItemsCollected();
        aufraumen.GetComponent<Quest>().ItemsCollected();
        arbeit.GetComponent<Quest>().ItemsCollected();
        bucher.GetComponent<Quest>().ItemsCollected();
        supermarkt.GetComponent<Quest>().ItemsCollected();
    }
}

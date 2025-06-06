using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.SearchService;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [Header("ItemManager")]
    public bool isFound = false;
    int amount = 0;

    [Header("Items Lists")]
    public List<GameObject> homeItems;
    public List<GameObject> libraryItems;
    public List<GameObject> shoptems;
    public List<GameObject> storageItems;

    [Header("Quests")]
    public GameObject toDo;
    public GameObject winCanvas;
    public TextMeshProUGUI winText;
    public GameObject loseCanvas;
    public TextMeshProUGUI loseText;
    private int amountOfQuest = 0;

    [Header("Day 1 Quests")]
    public bool quest1Day1 = false;
    public bool quest2Day1 = false;
    public bool quest3Day1 = false;

    [Header("Day 2 Quests")]
    public bool quest1Day2 = false;
    public bool quest2Day2 = false;
    public bool quest3Day2 = false;

    [Header("Day 3 Quests")]
    public bool quest1Day3 = false;
    public bool quest2Day3 = false;
    public bool quest3Day3 = false;

    [Header("Day 4 Quests")]
    public bool quest1Day4 = false;
    public bool quest2Day4 = false;
    public bool quest3Day4 = false;

    [Header("Day 2 Quests")]
    public bool quest1Day5 = false;
    public bool quest2Day5 = false;
    public bool quest3Day5 = false;

    private void Start()
    {
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);

        // homeItems = new PrefabAssetType[] { aubergine, brokkoli, butter, eier, fisch, knoblauch, käse, reis, salzPfeffer, zwiebel };
    }

    public void ItemFound()
    {
        amount += 1;
        Debug.Log("Item found " + amount);
    }

    public void UpdateToDo()
    {
        /*
         * if (EndOfQuest())
        {
            amountOfQuest += 1;
        }
        */
    }

    public void EndDay1()       // vom Ende der Szene ausgelöst
    {
        if (quest1Day1 == true &&  quest2Day1 == true && quest3Day1 == true)
            winCanvas.SetActive(true);
        else
            loseCanvas.SetActive(true);
    }
    public void EndDay2()       // vom Ende der Szene ausgelöst
    {
        if (quest1Day2 == true && quest2Day2 == true && quest3Day2 == true)
            winCanvas.SetActive(true);
        else
            loseCanvas.SetActive(true);
    }
    public void EndDay3()       // vom Ende der Szene ausgelöst
    {
        if (quest1Day3 == true && quest2Day3 == true && quest3Day3 == true)
            winCanvas.SetActive(true);
        else
            loseCanvas.SetActive(true);
    }
    public void EndDay4()       // vom Ende der Szene ausgelöst
    {
        if (quest1Day4 == true && quest2Day4 == true && quest3Day4 == true)
            winCanvas.SetActive(true);
        else
            loseCanvas.SetActive(true);
    }
    public void EndDay5()       // vom Ende der Szene ausgelöst
    {
        if (quest1Day5 == true && quest2Day5 == true && quest3Day5 == true)
        {
            winCanvas.SetActive(true);
            winText.text = amountOfQuest + "out of 3 Today's Quests completed";
        }
        else
        {
            loseCanvas.SetActive(true);
            loseText.text = amountOfQuest + "out of 3 Today's Quests completed";
        }
    }
}

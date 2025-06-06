

// Mini-games
//		-5 Standorte
//		- 3 Games
//		    - Leuten ausweichen: in verschiedenen Szenarien
//			    - beim hinreisen, Gassi gehen, Arbeit - ausweichen, gegner werden magnetisch angezogen, aus dem Radius bleiben
//		    	- Mitbewohner tun etwas und laufen in den Weg
//		    	- Sachen sammeln und an einen anderen Ort bringen
//	    	- Dialog skippen: Balken mit tasten druck aufbauen um Mut zu haben, das Gespräch zu beenden
//	    	- Lager Boxen verpacken

//	- trigger radius um Personen die man kennt
//	- vers.Taktiken Kopfhörer, Handy (kann selber zu viel verlieren)
//	- je nach Persönlichkeit/ Gruppenanzahl/ Beziehung mehr/weniger Energie
//	- reise minigame - Orte rushen vorbei, man muss Personen (vers. Persönlichkeiten, manche schneller) ausweichen - failure > Konversation


// - WASD-Steuerung
// - isometrische feste Camera, Stardew Valley

// - rendering: weniger Saturation mit weniger Energie

// - Gameplay: suchen, ausweichen & dialog (Minigame: Balken mit spammen auffüllen, um Dialog zu skippen + mini-cat label)
//		- Aufgaben zuhause, draußen und vormittags, nachmittags, abends
//		- einkaufen, Personen aus dem Weg gehen usw. mit Timer oder als Minigame

// - Tag(=Level) ist vorbei wenn man verliert
//		- Aufgaben stauen sich auf den nächsten Tag
//		- haben Konsequenzen > andere beschweren sich
//		- wenn es zu viele sind > overwhelmed > game failed
/*
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.SearchService;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /*
    [Header("Quests")]
    public List<GameObject> toDo;
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
    }*/

    /*
    public void UpdateToDo()
    {
        if (quest1Day1 == true)
        {

        }

        /*
         * if (EndOfQuest())
        {
            amountOfQuest += 1;
        }
        *//*
    }

    public void Day1()
    {
        
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
    }*/ /*
} */
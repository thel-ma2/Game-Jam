using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;

    [Header("Items")]
    public GameObject item1;

    [Header("UI")]
    public Canvas startMenu;
    public Canvas map;
    public Canvas winDisplay;
    public Canvas loseDisplay;

    public Canvas gameOverlay;
    [SerializeField] private GameObject dialogOverlay;
    [SerializeField] private Canvas pauseOverlay;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void DialogueStarted()       // vom player ausl�sen
    {
        player.gameObject.GetComponent<CharacterController>().enabled = false;
        dialogOverlay.SetActive(true);
    }
    public void DialogueEnded()
    {
        player.gameObject.GetComponent<CharacterController>().enabled = true;
        dialogOverlay.SetActive(false);
    }

    public void QuestCompleted()
    {

    }

    public void QuestFailed()
    {

    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Items")]
    public GameObject item1;

    [Header("UI")]
    [SerializeField] private Canvas startMenü;
    public Canvas gameOverlay;
    [SerializeField] private Canvas pauseSceen;
    [SerializeField] private Canvas map;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

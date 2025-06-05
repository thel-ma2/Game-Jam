using Unity.VisualScripting;
using UnityEngine;

public class ReisenPlayer : MonoBehaviour
{
    private BoxCollider2D Collider;
    private GameObject currentPerson;

    public SocialBatterie socialBattery;          // Referenz auf die Batterie
    public int conversationDrainAmount = 5;       // Wie viel die Batterie bei Gespräch verliert

    [Header("Numbers")]
    private int kPressCount = 0;
    [SerializeField] private int requiredKPressCount = 5;
    public bool conversation = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (conversation == false)
        {
            if (Input.GetKeyDown("w") && transform.position.z < 154 || Input.GetKeyDown(KeyCode.UpArrow) && transform.position.z < 154)
                transform.Translate(0, 0, +68, Space.World);

            if (Input.GetKeyDown("s") && transform.position.z > -118 || Input.GetKeyDown(KeyCode.DownArrow) && transform.position.z > -118)
                transform.Translate(0, 0, -68, Space.World);
        }

        if (Input.GetKeyDown("k"))
        {
            kPressCount++;

            if (kPressCount >= requiredKPressCount)
                EndConversation();
        }
    }

    void EndConversation()
    {
        if (currentPerson != null)
        {
            currentPerson.GetComponent<People>().EndConversation();
        }

        conversation = false;
        kPressCount = 0;

        if (socialBattery != null)
        {
            socialBattery.current -= conversationDrainAmount;
            if (socialBattery.current < socialBattery.minimum)
                socialBattery.current = socialBattery.minimum;
        }

        Debug.Log("Conversation ended, player can continue.");
    }

    public void StartConversation(GameObject person)     // wird vom 'People.cs' aufgerufen
    {
        currentPerson = person;
        conversation = true;
        Debug.Log("Entered a Conversation");
    }
}
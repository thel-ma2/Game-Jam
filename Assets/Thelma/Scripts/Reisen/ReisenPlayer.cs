using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ReisenPlayer : MonoBehaviour
{
    private GameObject currentPerson;
    private GameObject background;
    private GameObject background2;
    private GameObject peopleSpawner;

    // Input System
    private InputSystem_Actions playerControls;
    private Vector2 movementInput;

    private CharacterController myCharacterController;

    [Header("Numbers")]
    private int kPressCount = 0;
    [SerializeField] private int requiredKPressCount = 5;
    [SerializeField] private float moveSpeed = 2f;
    public bool conversation = false;

    private BurnoutScale socialBattery;  // socialBattery automatisch zugewiesen
    [SerializeField] private int conversationDrainAmount = 1;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        myCharacterController = GetComponent<CharacterController>();

        // Automatisch das GameObject mit Namen "Progress Bar" finden und BurnoutScale zuweisen
        GameObject progressBarObj = GameObject.Find("Progress Bar");
        if (progressBarObj != null)
        {
            socialBattery = progressBarObj.GetComponent<BurnoutScale>();
            if (socialBattery == null)
                Debug.LogWarning("BurnoutScale-Komponente nicht auf 'Progress Bar' gefunden.");
        }
        else
        {
            Debug.LogWarning("GameObject mit Namen 'Progress Bar' wurde nicht gefunden.");
        }

        playerControls.Enable();
    }

    void Start()
    {
        transform.position = new Vector3(-100, 0, 18);

        background = GameObject.FindWithTag("Background");
        background2 = GameObject.FindWithTag("Background2");
        peopleSpawner = GameObject.FindWithTag("PeopleSpawner");

        playerControls.Player.Move.performed += OnMovementPerformed;
        playerControls.Player.Move.canceled += OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    void Update()
    {
        if (conversation == false)
            Move();

        if (Input.GetKeyDown("k"))
        {
            kPressCount++;

            if (kPressCount >= requiredKPressCount)
                EndConversation();
        }
    }

    private void Move()
    {
        if (transform.position.z <= 154 && transform.position.z >= -118)
        {
            Vector3 moveDirection = new Vector3(0, 0, movementInput.y);
            myCharacterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        else if (transform.position.z >= 154)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
        else if (transform.position.z <= -118)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
    }

    void EndConversation()
    {
        if (currentPerson != null)
        {
            currentPerson.GetComponent<People>().EndConversation();
        }

        if (background != null)
            background.GetComponent<Background>().ResumeMovement();

        if (background2 != null)
            background2.GetComponent<Background>().ResumeMovement();

        if (peopleSpawner != null)
            peopleSpawner.GetComponent<PeopleSpawner>().ResumeMovement();

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

    public void StartConversation(GameObject person)
    {
        currentPerson = person;
        conversation = true;
        Debug.Log("Entered a Conversation");
    }
}

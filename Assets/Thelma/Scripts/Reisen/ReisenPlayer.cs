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

    //Input System
    private InputSystem_Actions playerControls;
    private Vector2 movementInput;

    private CharacterController myCharacterController;

    [Header("Numbers")]
    private int kPressCount = 0;
    [SerializeField] private int requiredKPressCount = 5;
    [SerializeField] private float moveSpeed = 2f;
    public bool conversation = false;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        myCharacterController = GetComponent<CharacterController>();

        playerControls.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3 (-100, 0, 18);

        background = GameObject.FindWithTag("Background");
        background2 = GameObject.FindWithTag("Background2");
        peopleSpawner = GameObject.FindWithTag("PeopleSpawner");

        playerControls.Player.Move.performed += OnMovementPerformed;
        playerControls.Player.Move.canceled += OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        // Read the movement value when the action is performed
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // Reset movement when the action is canceled
        movementInput = Vector2.zero;
    }

    // Update is called once per frame
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
        currentPerson.GetComponent<People>().EndConversation();
        background.GetComponent<Background>().ResumeMovement();
        background2.GetComponent<Background>().ResumeMovement();
        peopleSpawner.GetComponent<PeopleSpawner>().ResumeMovement();
        conversation = false;
        Debug.Log("Conversation ended, player can continue.");
    }

    public void StartConversation(GameObject person)     // wird vom 'People.cs' aufgerufen
    {
        currentPerson = person;
        conversation = true;
        Debug.Log("Entered a Conversation");
    }
}
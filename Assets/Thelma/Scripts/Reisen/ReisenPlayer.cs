using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReisenPlayer : MonoBehaviour
{
    private BoxCollider Collider;
    private GameObject currentPerson;
    private GameObject gameManager;

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
        Collider = GetComponent<BoxCollider>();
        gameManager = GameObject.FindWithTag("GameController");

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
        // if (Input.GetKeyDown("w") && transform.position.z < 154 || Input.GetKeyDown(KeyCode.UpArrow) && transform.position.z < 154)

        // if (Input.GetKeyDown("s") && transform.position.z > -118 || Input.GetKeyDown(KeyCode.DownArrow) && transform.position.z > -118)

        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);

        myCharacterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void EndConversation()
    {
        currentPerson.GetComponent<People>().EndConversation();
        conversation = false;
        Debug.Log("Conversation ended, player can continue.");
    }

    public void StartConversation(GameObject person)     // wird vom 'People.cs' aufgerufen
    {
        currentPerson = person;
        conversation = true;
        Debug.Log("Entered a Conversation");
        gameManager.GetComponent<GameManager>().DialogueStarted();
    }
}
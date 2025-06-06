using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class People : MonoBehaviour
{
    //Input System
    private InputSystem_Actions playerControls;
    private Vector2 movementInput;

    private CharacterController myCharacterController;

    [SerializeField] private float moveSpeed = 5;
    float deadZone = -350;

    BoxCollider myTrigger;
    private bool conversation = false;
    private GameObject background;
    private GameObject background2;
    private GameObject peopleSpawner;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        myCharacterController = GetComponent<CharacterController>();

        playerControls.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTrigger = GetComponent<BoxCollider>();
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
        // transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        if (conversation == false)
            Move();

        if (transform.position.x < deadZone )
            Destroy(gameObject);
    }

    void Move()
    {
        if (conversation == false)
        {
            Vector3 moveDirection = new Vector3(-2, 0, 0);

            myCharacterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            conversation = false;
            StartConversation(collider.gameObject);
        }
    }

    void StartConversation(GameObject player)
    {
        conversation = true;
        player.GetComponent<ReisenPlayer>().StartConversation(gameObject);
        background.GetComponent<Background>().StopMovement();
        background2.GetComponent<Background>().StopMovement();
        peopleSpawner.GetComponent<PeopleSpawner>().StopMovement();
        myTrigger.enabled = false;
    }

    public void EndConversation()
    {
        conversation = false;
    }
}

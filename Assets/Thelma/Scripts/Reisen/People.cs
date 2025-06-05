using UnityEngine;
using UnityEngine.InputSystem;

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
        Vector3 moveDirection = new Vector3(-2, 0, 0);

        myCharacterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && conversation == false)
        {
            if (collider.gameObject.GetComponent<ReisenPlayer>().conversation == false)
                StartConversation(collider.gameObject);
        }
    }

    void StartConversation(GameObject player)
    {
        conversation = true;
        player.GetComponent<ReisenPlayer>().StartConversation(gameObject);
        myTrigger.enabled = false;
    }

    public void EndConversation()
    {
        conversation = false;
        Destroy(gameObject);
    }
}

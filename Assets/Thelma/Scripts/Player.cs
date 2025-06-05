using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [Header("Soziale Batterie")]
    public SocialBatterie socialBattery;           // Referenz zur Batterieanzeige
    public int conversationDrainAmount = 10;       // Wie viel bei einer Konversation verloren geht


    // grab/ take > sammeln + an Ziel ablegen
    // eat

    //Input System
    private InputSystem_Actions playerControls;
    private Vector2 movementInput;

    private GameObject currentMitbewohner;

    private CharacterController myCharacterController;
    private AudioSource audioSource;

    [Header("Audio")]
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip cookingSound;
    [SerializeField] private AudioClip eatingSound;
    [SerializeField] private AudioClip talkingSound;

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
        audioSource = GetComponent<AudioSource>();

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

    void Move()
    {
        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);

        myCharacterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        /*
        if (!audioSource.isPlaying && movementInput != Vector2.zero)
        {
            audioSource.PlayOneShot(walkingSound);
            Debug.Log("tap tap tap ...");
        }
        */
    }

    void EndConversation()
    {
        if (currentMitbewohner != null)
        {
            currentMitbewohner.GetComponent<Mitbewohner>().EndConversation();
        }

        conversation = false;
        kPressCount = 0;

        // Soziale Batterie reduzieren
        if (socialBattery != null)
        {
            socialBattery.current -= conversationDrainAmount;

            if (socialBattery.current < socialBattery.minimum)
                socialBattery.current = socialBattery.minimum;
        }

        Debug.Log("Conversation ended, player can continue.");
    }


    public void StartConversation(GameObject mitbewohner)     // wird vom Mitbewohner aufgerufen
    {
        currentMitbewohner = mitbewohner;
        conversation = true;
        Debug.Log("Entered a Conversation");
    }

}

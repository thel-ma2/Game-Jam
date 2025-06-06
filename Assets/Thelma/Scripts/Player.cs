using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Soziale Batterie")]
    // Referenz auf die SocialBatterie, wird automatisch im Awake() gesetzt
    public SocialBatterie socialBattery;

    // Wie viel Energie bei einer Konversation verloren geht
    public int conversationDrainAmount = 10;

    // Eingabesteuerungssystem (Unity Input System)
    private InputSystem_Actions playerControls;

    // Bewegungseingabe (2D Vektor)
    private Vector2 movementInput;

    // Referenz auf den aktuellen Mitbewohner, mit dem gesprochen wird
    private GameObject currentMitbewohner;

    // Charakter-Controller zum Bewegen des Spielers
    private CharacterController myCharacterController;

    // Audioquelle f�r Soundeffekte
    private AudioSource audioSource;

    [Header("Audio")]
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip cookingSound;
    [SerializeField] private AudioClip eatingSound;
    [SerializeField] private AudioClip talkingSound;

    [Header("Numbers")]
    // Anzahl der "K"-Tastendr�cke gez�hlt w�hrend einer Konversation
    private int kPressCount = 0;

    // Wie viele "K"-Tastendr�cke notwendig sind, um Konversation zu beenden
    [SerializeField] private int requiredKPressCount = 5;

    // Geschwindigkeit, mit der sich der Spieler bewegt
    [SerializeField] private float moveSpeed = 2f;

    // Flag, ob sich der Spieler gerade in einer Konversation befindet
    public bool conversation = false;

    // Awake wird aufgerufen, sobald das Skript geladen wird
    private void Awake()
    {
        // Initialisierung der Eingabesteuerung
        playerControls = new InputSystem_Actions();

        // Referenz auf den CharacterController holen (muss am selben GameObject sein)
        myCharacterController = GetComponent<CharacterController>();

        // Automatisch das GameObject mit dem Namen "Progress Bar" finden
        GameObject progressBarObj = GameObject.Find("Progress Bar");
        if (progressBarObj != null)
        {
            // Versuche die SocialBatterie-Komponente auf dem gefundenen GameObject zu holen
            socialBattery = progressBarObj.GetComponent<SocialBatterie>();

            // Warnung, falls die Komponente nicht gefunden wurde
            if (socialBattery == null)
            {
                Debug.LogWarning("SocialBatterie-Komponente wurde auf dem GameObject 'Progress Bar' nicht gefunden.");
            }
        }
        else
        {
            // Warnung, falls das GameObject "Progress Bar" nicht gefunden wurde
            Debug.LogWarning("GameObject mit Namen 'Progress Bar' wurde nicht gefunden.");
        }

        // Aktivieren der Eingabesteuerung
        playerControls.Enable();
    }

    // Start wird vor dem ersten Frame-Update aufgerufen
    private void Start()
    {
        // Hole die AudioSource-Komponente (f�r Soundeffekte)
        audioSource = GetComponent<AudioSource>();

        // Eingabe-Events abonnieren: Bewegung gestartet und gestoppt
        playerControls.Player.Move.performed += OnMovementPerformed;
        playerControls.Player.Move.canceled += OnMovementCanceled;
    }

    // Wird aufgerufen, wenn Bewegungsinput erkannt wird
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        // Lese den Bewegungsvektor aus der Eingabe
        movementInput = context.ReadValue<Vector2>();
    }

    // Wird aufgerufen, wenn Bewegungsinput aufh�rt (z.B. Loslassen der Tasten)
    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // Setze Bewegungseingabe auf Null (kein Input)
        movementInput = Vector2.zero;
    }

    // Update wird einmal pro Frame aufgerufen
    private void Update()
    {
        // Wenn keine Konversation l�uft, soll sich der Spieler bewegen k�nnen
        if (conversation == false)
        {
            Move();
        }

        // Wenn die Taste "k" gedr�ckt wird
        if (Input.GetKeyDown("k"))
        {
            kPressCount++;

            // Wenn genug oft gedr�ckt wurde, beende die Konversation
            if (kPressCount >= requiredKPressCount)
            {
                EndConversation();
            }
        }
    }

    // Bewegt den Spieler basierend auf der Bewegungseingabe
    private void Move()
    {
        // Erstelle Bewegungsvektor in 3D (x: seitlich, y: 0, z: vor/zur�ck)
        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);

        // Bewege den Charakter mit CharacterController
        myCharacterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        /*
        // Beispiel f�r Schrittger�usche (auskommentiert)
        if (!audioSource.isPlaying && movementInput != Vector2.zero)
        {
            audioSource.PlayOneShot(walkingSound);
            Debug.Log("tap tap tap ...");
        }
        */
    }

    // Beendet die aktuelle Konversation
    private void EndConversation()
    {
        // Falls ein Mitbewohner aktiv ist, beende seine Konversation
        if (currentMitbewohner != null)
        {
            currentMitbewohner.GetComponent<Mitbewohner>().EndConversation();
        }

        // Konversationsflag zur�cksetzen
        conversation = false;

        // Z�hler f�r Tastendr�cke zur�cksetzen
        kPressCount = 0;

        // Soziale Batterie reduzieren, falls zugewiesen
        if (socialBattery != null)
        {
            socialBattery.current -= conversationDrainAmount;

            // Sicherstellen, dass Wert nicht unter Minimum f�llt
            if (socialBattery.current < socialBattery.minimum)
            {
                socialBattery.current = socialBattery.minimum;
            }
        }

        // Debug-Log ausgeben
        Debug.Log("Konversation beendet, Spieler kann weitermachen.");
    }

    // Wird vom Mitbewohner aufgerufen, um eine Konversation zu starten
    public void StartConversation(GameObject mitbewohner)
    {
        currentMitbewohner = mitbewohner;
        conversation = true;

        // Debug-Log ausgeben
        Debug.Log("Konversation gestartet.");
    }
}

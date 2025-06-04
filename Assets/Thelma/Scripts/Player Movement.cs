using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // left, right, "up", "down" > left/right are not equal speed to up/down
    // grab/ take > sammeln + an Ziel ablegen
    // eat
    // Mitbewohner, die etwas anderes machen + mit Trigger Radius

    private AudioSource audioSource;

    [Header("Audio")]
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip cookingSound;
    [SerializeField] private AudioClip eatingSound;
    [SerializeField] private AudioClip talkingSound;


    Transform Transform;

    [Header("Numbers")]
    private bool playerCanMove = true;
    private bool conversation = false;
    private int kPressCount = 0;
    [SerializeField] private int requiredKPressCount = 5;       // if changed, change at Mitbewohner too


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCanMove == true)
        {
            Move();
            if (!audioSource.isPlaying && IsMoving())
            {
                audioSource.PlayOneShot(walkingSound);
                Debug.Log("tap tap tap ...");
            }
        }

        if (conversation == true)
        {
            Debug.Log("Entered a Conversation");
            playerCanMove = false;
        }

        if (Input.GetKeyDown("k"))
        {
            kPressCount++;

            if (kPressCount >= requiredKPressCount)
                EndConversation();
        }
    }

    void Move()
    {
        if (Input.GetKey("w"))
            transform.Translate(0, 0, +1, Space.World);

        if (Input.GetKey("a"))
            transform.Translate(-1, 0, 0, Space.World);

        if (Input.GetKey("s"))
            transform.Translate(0, 0, -1, Space.World);

        if (Input.GetKey("d"))
            transform.Translate(+1, 0, 0, Space.World);
    }

    bool IsMoving()
    {
        return Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
    }

    void OnTriggerEnter(Collider other)
    {
        conversation = true;
        kPressCount = 0;
    }

    void EndConversation()
    {
        conversation = false;
        playerCanMove = true;
        Debug.Log("Conversation ended, player can continue.");
    }

}

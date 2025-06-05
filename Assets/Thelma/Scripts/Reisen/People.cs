using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    float deadZone = -350;

    BoxCollider myTrigger;
    private bool conversation = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTrigger = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone )
            Destroy(gameObject);
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

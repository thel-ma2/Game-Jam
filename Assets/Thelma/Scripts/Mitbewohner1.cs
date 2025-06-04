using UnityEngine;
using UnityEngine.LightTransport;

public class Mitbewohner1 : MonoBehaviour
{
    GameObject player;
    BoxCollider playerCollider;

    [SerializeField] private float speed = 2f;
    private bool conversation = false;
    private int kPressCount = 0;
    [SerializeField] private int requiredKPressCount = 5;       // if changed, change at Player too

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = player.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        while (conversation == false)
        {
            transform.Translate(-10 * speed * Time.deltaTime, 0, 0, Space.World);
            if (conversation == true)
                break;
        }

        if (Input.GetKeyDown("k"))
        {
            kPressCount++;

            if (kPressCount >= requiredKPressCount)
                conversation = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        conversation = true;
        kPressCount = 0;
    }

}

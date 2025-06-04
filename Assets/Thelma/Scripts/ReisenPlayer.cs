using Unity.VisualScripting;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private BoxCollider2D Collider;
    private Transform Transform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (transform.position.z < 115)
            // Input.GetKey("w") = true;
        if(Input.GetKeyDown("w"))
            transform.Translate(0, 0, + 50, Space.World);

        if (Input.GetKeyDown("s"))
            transform.Translate(0, 0, -50, Space.World);
    }
}

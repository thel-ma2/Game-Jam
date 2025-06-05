using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
    }
}

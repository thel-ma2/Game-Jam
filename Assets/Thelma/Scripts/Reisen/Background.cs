using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    public bool stop = false;

    // Update is called once per frame
    void Update()
    {
        if (stop == false)
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
    }

    public void StopMovement()
    {
        stop = true;
    }
    public void ResumeMovement()
    {
        stop = false;
    }
}

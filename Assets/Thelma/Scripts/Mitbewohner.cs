using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Mitbewohner : MonoBehaviour
{
    SphereCollider myTrigger;
    private bool conversation = false;

    //Components
    NavMeshAgent myAgent;
    public float moveRange = 500;
    public float chanceToStandAround = 10;
    private bool isStandingAround;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTrigger = GetComponent<SphereCollider>();
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isStandingAround == false)
            RandomWalk();
    }

    void RandomWalk()
    {
        //Return if already moving
        if (myAgent.pathPending || !myAgent.isOnNavMesh || myAgent.remainingDistance > 0.1f)
            return;

        if (Random.Range(0, 100) < chanceToStandAround)
        {
            //Stand Around
            StartCoroutine(StandAround());
        }
        else //Walk around
        {
            WalkAround();
        }
    }

    void WalkAround()
    {
        //Get Random Point and move there
        Vector3 randomDestination = GetRandomNavMeshPoint(transform.position, moveRange);
        myAgent.SetDestination(randomDestination);
    }
    IEnumerator StandAround()
    {
        isStandingAround = true;

        float idleTime = Random.Range(1f, 3f);
        yield return new WaitForSeconds(idleTime);

        myTrigger.enabled = true;

        isStandingAround = false;
    }

    Vector3 GetRandomNavMeshPoint(Vector3 center, float range, int maxTries = 10)
    {
        for (int i = 0; i < maxTries; i++)
        {
            // Pick a random point in a circle around the center
            Vector2 circle = Random.insideUnitCircle * range;
            Vector3 randomPoint = center + new Vector3(circle.x, 0, circle.y);

            // Try to find the nearest NavMesh point to that location
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.5f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        // Fallback to current position if no valid point was found
        return center;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && conversation == false)
        {
            if (collider.gameObject.GetComponent<Player>().conversation == false)
                StartConversation(collider.gameObject);
        }
    }

    void StartConversation(GameObject player)
    {
        isStandingAround = true;
        conversation = true;
        player.GetComponent<Player>().StartConversation(gameObject);
        myAgent.SetDestination(transform.position);
        myTrigger.enabled = false;
    }

    public void EndConversation()
    {
        conversation = false;

        StartCoroutine(StandAround());
    }

}

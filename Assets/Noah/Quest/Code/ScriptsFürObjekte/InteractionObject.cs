using UnityEngine;

public class InteractionRequirement : MonoBehaviour
{
    public CollectMultipleItemsGoal linkedQuestGoal;

    // Beispiel: Interaktion mit Taste E in Reichweite
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (linkedQuestGoal != null)
            {
                linkedQuestGoal.OnInteractionDone();
                Debug.Log("Interaktion abgeschlossen - Quest kann beendet werden.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}

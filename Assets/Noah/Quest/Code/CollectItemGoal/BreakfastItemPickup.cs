using UnityEngine;

// Script an einem Item-GameObject, das eingesammelt werden kann
public class BreakfastItemPickup : MonoBehaviour
{
    public BreakfastItem Item;           // Referenz zum ScriptableObject (im Inspector setzen)
    public PlayerInventory PlayerInventory; // Referenz auf das Inventar des Spielers

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.CollectItem(Item); // Item einsammeln
            Destroy(gameObject);                // Objekt entfernen
        }
    }
}

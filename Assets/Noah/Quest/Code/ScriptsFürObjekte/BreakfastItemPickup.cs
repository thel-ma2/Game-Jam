using UnityEngine;

public class BreakfastItemPickup : MonoBehaviour
{
    public BreakfastItem Item;
    public PlayerInventory PlayerInventory;

    [Header("Pickup Optionen")]
    public bool pickupOnTouch = true;
    public bool pickupOnLookAndKey = true;
    public KeyCode pickupKey = KeyCode.E;
    public float lookPickupDistance = 3f;

    [Header("Entfernen nach Aufheben")]
    public bool destroyAfterPickup = true; // <-- NEU: Kontrolle im Inspector

    private bool isLookedAt = false;

    private void OnTriggerEnter(Collider other)
    {
        if (pickupOnTouch && other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Update()
    {
        if (pickupOnLookAndKey)
        {
            CheckLookAndPickup();
        }

        if (!isLookedAt)
        {
            PickupHintUIManager.Instance?.HideHint();
        }
    }

    private void CheckLookAndPickup()
    {
        isLookedAt = false;

        Camera cam = Camera.main;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, lookPickupDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isLookedAt = true;

                PickupHintUIManager.Instance?.ShowHint("Drücke E zum Aufsammeln");

                if (Input.GetKeyDown(pickupKey))
                {
                    Collect();
                }
            }
        }
    }

    private void Collect()
    {
        PickupHintUIManager.Instance?.HideHint();
        PlayerInventory.CollectItem(Item);

        if (destroyAfterPickup)
        {
            Destroy(gameObject);
        }
        else
        {
            // Nur Deaktivieren der Abholung
            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = false;

            this.enabled = false; // Deaktiviert das Script selbst
        }
    }
}

using UnityEngine;

public class ClockHand : MonoBehaviour
{
    [Header("Rotationseinstellungen")]
    public float angle;            // Aktueller Winkel in Grad (0 = oben, 90 = rechts)
    public bool autoRotate = false;
    public float rotationSpeed = 30f; // Grad pro Sekunde (nur bei Auto-Rotation)

    void Update()
    {
        if (autoRotate)
        {
            angle += rotationSpeed * Time.deltaTime;
            angle %= 360f; // bleibt im Bereich 0–360
        }

        // Zeiger rotieren
        transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
    }
}

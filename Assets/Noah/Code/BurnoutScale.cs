using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class BurnoutScale : MonoBehaviour
{
    [Header("Wertebereich")]
    public int minimum = 0;
    public int maximum = 100;
    [Range(0, 100)]
    public int current = 50;

    [Header("X-Positionsgrenzen (Editor-basiert)")]
    public float minimumX = 0f;  // Position ganz links
    public float maximumX = 200f; // Position ganz rechts

    [Header("UI-Elemente")]
    public RectTransform pointer;
    public Color color = Color.white;

    void Update()
    {
        if (pointer == null)
        {
            return; // Falls kein Zeiger gesetzt wurde, abbrechen
        }

        if (maximum == minimum)
        {
            return; // Division durch Null vermeiden
        }

        UpdatePointerPosition();
    }

    void UpdatePointerPosition()
    {
        // Berechne, wie weit der aktuelle Wert zwischen Minimum und Maximum liegt (als Wert zwischen 0 und 1)
        float t = (float)(current - minimum) / (maximum - minimum);

        // Clamp auf 0 bis 1, um Über-/Unterlauf zu verhindern
        t = Mathf.Clamp01(t);

        // Da rechts das Minimum sein soll und links das Maximum, drehen wir den Wert um
        // Wenn t = 0 (current = minimum), soll der Zeiger an maximumX (rechts) sein
        // Wenn t = 1 (current = maximum), soll der Zeiger an minimumX (links) sein
        float invertedT = 1f - t;

        // Position zwischen minimumX (links) und maximumX (rechts) berechnen anhand des invertierten Wertes
        float neueXPosition = Mathf.Lerp(minimumX, maximumX, invertedT);

        // Die neue Position als AnchoredPosition für das RectTransform setzen
        Vector2 aktuellePosition = pointer.anchoredPosition;
        aktuellePosition.x = neueXPosition;
        pointer.anchoredPosition = aktuellePosition;

        // Optional: Farbe des Zeigers setzen, falls ein Image-Component dran ist
        Image zeigerBild = pointer.GetComponent<Image>();
        if (zeigerBild != null)
        {
            zeigerBild.color = color;
        }
    }
}

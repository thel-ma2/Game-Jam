using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class BraveryScale: MonoBehaviour
{
    [Header("Wertebereich")]
    public int minimum = 0;
    public int maximum = 100;
    [Range(0, 100)]
    public int current = 50;

    [Header("Y-Positionsgrenzen (Editor-basiert)")]
    public float minimumY = 0f;   // z.B. -100
    public float maximumY = 200f; // z.B. +100

    [Header("UI-Elemente")]
    public RectTransform pointer;
    public Color color = Color.white;

    void Update()
    {
        if (pointer == null || maximum == minimum)
            return;

        UpdatePointerPosition();
    }

    void UpdatePointerPosition()
    {
        // Prozentwert berechnen
        float t = Mathf.Clamp01((float)(current - minimum) / (maximum - minimum));

        // Interpolation zwischen min und max Y
        float newY = Mathf.Lerp(minimumY, maximumY, t);

        // Zuweisung an Pointer
        Vector2 anchoredPos = pointer.anchoredPosition;
        anchoredPos.y = newY;
        pointer.anchoredPosition = anchoredPos;

        // Optional: Farbe setzen
        Image img = pointer.GetComponent<Image>();
        if (img != null)
            img.color = color;
    }
}

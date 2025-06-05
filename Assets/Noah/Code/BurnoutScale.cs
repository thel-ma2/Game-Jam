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
    public float minimumX = 0f;  
    public float maximumX = 200f; 

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

        // Interpolation zwischen min und max X
        float newX = Mathf.Lerp(minimumX, maximumX, t);

        // X-Position setzen
        Vector2 anchoredPos = pointer.anchoredPosition;
        anchoredPos.x = newX;
        pointer.anchoredPosition = anchoredPos;

        // Optional: Farbe setzen
        Image img = pointer.GetComponent<Image>();
        if (img != null)
            img.color = color;
    }
}

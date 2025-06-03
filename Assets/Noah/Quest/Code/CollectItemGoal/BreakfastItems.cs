using UnityEngine;

// Definiert ein Fr�hst�cksitem mit Name, Icon und Wert
[CreateAssetMenu(menuName = "Items/BreakfastItem")]
public class BreakfastItem : ScriptableObject
{
    public string ItemName;      // Name des Items (z.B. "Br�tchen")
    public Sprite Icon;          // Icon f�r UI-Anzeige
    public int Value = 1;        // Beliebiger Wert (z.B. Punkte oder Preis)
}

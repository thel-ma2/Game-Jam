using UnityEngine;

// Definiert ein Frühstücksitem mit Name, Icon und Wert
[CreateAssetMenu(menuName = "Items/BreakfastItem")]
public class BreakfastItem : ScriptableObject
{
    public string ItemName;      // Name des Items (z.B. "Brötchen")
    public Sprite Icon;          // Icon für UI-Anzeige
    public int Value = 1;        // Beliebiger Wert (z.B. Punkte oder Preis)
}

using UnityEngine;

public class BurnoutScaleInput : MonoBehaviour
{
    public BurnoutScale burnoutScale; // Referenz zum BurnoutScale-Skript
    public int step = 1;               // Schrittgröße bei Tastendruck

    void Update()
    {
        if (burnoutScale == null)
            return;

        // Rechts-Taste: Wert verringern, Richtung Minimum (rechts)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            burnoutScale.current = Mathf.Clamp(
                burnoutScale.current - step,      // Wert runter (Minimum liegt rechts)
                burnoutScale.minimum,
                burnoutScale.maximum
            );
        }

        // Links-Taste: Wert erhöhen, Richtung Maximum (links)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            burnoutScale.current = Mathf.Clamp(
                burnoutScale.current + step,      // Wert rauf (Maximum liegt links)
                burnoutScale.minimum,
                burnoutScale.maximum
            );
        }
    }
}

using UnityEngine;

public class BraveryScaleInput : MonoBehaviour
{
    public BraveryScale braveryScale; // Referenz zum anderen Script
    public int step = 1;              // Schrittgröße bei Tastendruck

    void Update()
    {
        if (braveryScale == null)
            return;

        // Nach oben
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            braveryScale.current = Mathf.Clamp(
                braveryScale.current + step,
                braveryScale.minimum,
                braveryScale.maximum
            );
        }

        // Nach unten
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            braveryScale.current = Mathf.Clamp(
                braveryScale.current - step,
                braveryScale.minimum,
                braveryScale.maximum
            );
        }
    }
}

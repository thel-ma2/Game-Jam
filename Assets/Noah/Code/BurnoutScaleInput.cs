using UnityEngine;

public class BurnoutScaleInput : MonoBehaviour
{
    public BurnoutScale burnoutScale; // Referenz zum anderen Script
    public int step = 1;              // Schrittgröße bei Tastendruck

    void Update()
    {
        if (burnoutScale == null)
            return;

        // Nach rechts
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            burnoutScale.current = Mathf.Clamp(
                burnoutScale.current + step,
                burnoutScale.minimum,
                burnoutScale.maximum
            );
        }

        // Nach links
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            burnoutScale.current = Mathf.Clamp(
                burnoutScale.current - step,
                burnoutScale.minimum,
                burnoutScale.maximum
            );
        }
    }
}

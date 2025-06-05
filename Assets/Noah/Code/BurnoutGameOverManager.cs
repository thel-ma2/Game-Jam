using UnityEngine;

public class BurnoutGameOverManager : MonoBehaviour
{
    public BurnoutScale burnoutScale;    // Referenz auf dein BurnoutScale-Script
    public GameObject gameOverCanvas;    // Canvas, das bei Game Over aktiviert wird

    private bool gameOverShown = false;  // Damit das Canvas nur einmal aktiviert wird

    void Update()
    {
        if (burnoutScale == null || gameOverCanvas == null)
            return;

        // Prüfe, ob current auf 0 ist oder darunter (kann je nach Logik auch == 0 sein)
        if (burnoutScale.current <= burnoutScale.minimum && !gameOverShown)
        {
            gameOverCanvas.SetActive(true);
            gameOverShown = true;
        }
    }
}

using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public SocialBatterie batterieBar;  // Referenz auf dein SocialBatterie-Script
    public int damageAmount = 10;       // Wie viel Schaden bei jedem Treffer abgezogen wird

    void Update()
    {
        // Zum Test: Wenn du die Leertaste drückst, wird Schaden verursacht
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(int damage)
    {
        if (batterieBar == null) return;

        // Lebenspunkte abziehen, aber nicht unter minimum fallen lassen
        batterieBar.current -= damage;
        if (batterieBar.current < batterieBar.minimum)
        {
            batterieBar.current = batterieBar.minimum;
        }
    }
}

using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public SocialBatterie batterieBar;     // Referenz auf dein SocialBatterie-Script
    public int damageAmount = 10;          // Schaden bei jedem Treffer
    public int healAmount = 10;            // Wie viel beim Heilen wiederhergestellt wird

    void Update()
    {
        // Leertaste = Schaden nehmen
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(damageAmount);
        }

        // H-Taste = heilen
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(healAmount);
        }
    }

    public void TakeDamage(int damage)
    {
        if (batterieBar == null) return;

        batterieBar.current -= damage;
        if (batterieBar.current < batterieBar.minimum)
        {
            batterieBar.current = batterieBar.minimum;
        }
    }

    public void Heal(int amount)
    {
        if (batterieBar == null) return;

        batterieBar.current += amount;
        if (batterieBar.current > batterieBar.maximum)
        {
            batterieBar.current = batterieBar.maximum;
        }
    }
}

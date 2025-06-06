using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BurnoutMeterManager : MonoBehaviour
{
    public BurnoutScale burnoutScale;
    public Image pointerImage;
    public Color flashColor = Color.red;
    public float flashDuration = 0.2f;

    public AudioSource audioSource;
    public AudioClip burnoutIncreaseSound;

    private Color originalColor;

    private void Start()
    {
        if (pointerImage != null)
        {
            originalColor = pointerImage.color;
        }
    }

    // Diese Methode kannst du manuell oder aus einem anderen Script aufrufen
    public void ErhöheBurnoutWert(int menge = 1)
    {
        if (burnoutScale == null)
        {
            Debug.LogWarning("BurnoutScale ist nicht gesetzt.");
            return;
        }

        burnoutScale.current = Mathf.Clamp(
            burnoutScale.current + menge,
            burnoutScale.minimum,
            burnoutScale.maximum
        );

        Debug.Log($"Burnout wurde um {menge} erhöht.");

        if (pointerImage != null)
            StartCoroutine(FlashPointer());

        if (audioSource != null && burnoutIncreaseSound != null)
            audioSource.PlayOneShot(burnoutIncreaseSound);
    }

    private IEnumerator FlashPointer()
    {
        pointerImage.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        pointerImage.color = originalColor;
    }
}

using UnityEngine;
using TMPro;

public class PickupHintUIManager : MonoBehaviour
{
    public static PickupHintUIManager Instance;
    public TMP_Text hintText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (hintText != null)
        {
            hintText.gameObject.SetActive(true); // Immer sichtbar
            hintText.text = ""; // Start leer
        }
    }

    public void ShowHint(string message)
    {
        if (hintText != null)
        {
            hintText.text = message;
            hintText.gameObject.SetActive(true);
        }
    }

    public void HideHint()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false);
        }
    }
}

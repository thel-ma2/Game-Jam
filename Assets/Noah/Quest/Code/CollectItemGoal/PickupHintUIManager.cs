using UnityEngine;
using TMPro;

public class PickupHintUIManager : MonoBehaviour
{
    public static PickupHintUIManager Instance;

    public TMP_Text hintText;

    private void Awake()
    {
        // Singleton-Setup
        if (Instance == null)
        {
            Instance = this;
            hintText.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
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

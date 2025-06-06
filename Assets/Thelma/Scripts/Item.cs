using TMPro;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemManager manager;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            manager.GetComponent<ItemManager>().isFound = true;
            manager.GetComponent<ItemManager>().ItemFound();
        }
    }
}

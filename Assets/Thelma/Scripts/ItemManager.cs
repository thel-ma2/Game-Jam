using UnityEditor.Search;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public bool isFound = false;
    int amount = 0;

    public GameObject aubergine;
    public GameObject brokkoli;
    public GameObject butter;
    public GameObject eier;
    public GameObject fisch;
    public GameObject knoblauch;
    public GameObject käse;
    public GameObject reis;
    public GameObject salzPfeffer;
    public GameObject zwiebel;

    public void ItemFound()
    {
        amount += 1;
        Debug.Log("Item found " + amount);
    }
}

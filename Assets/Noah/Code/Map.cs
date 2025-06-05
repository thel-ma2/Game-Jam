using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
   
    public void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void LoadLibrary()
    {
        SceneManager.LoadScene("Library");
    }

    public void LoadPark()
    {
        SceneManager.LoadScene("Park");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void LoadWork()
    {
        SceneManager.LoadScene("Work");
    }

}

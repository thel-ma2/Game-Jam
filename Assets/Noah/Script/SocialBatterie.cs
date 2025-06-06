using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode()]
public class SocialBatterie : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


 
    public int minimum;
    public int maximum;
    public int current;
    public Image mask;
    public Image fill;
    public Color color;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill(GetFill());
    }

    private Image GetFill()
    {
        return fill;
    }

    void GetCurrentFill(Image fill)
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;

        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
        fill.color = color;

    }
}

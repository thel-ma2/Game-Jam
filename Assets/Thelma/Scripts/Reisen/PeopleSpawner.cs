using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject person1;
    public GameObject person2;
    public GameObject person3;
    public GameObject person4;
    public GameObject person5;
    public GameObject person6;
    public GameObject person7;
    public GameObject person8;
    public GameObject person9;
    public GameObject person10;

    private GameObject[] people;

    [SerializeField] private float spawnRate = 5;
    private float timer = 1;

    private int offset1 = 136;
    private int offset2 = 68;
    private int offset3 = -68;
    private int offset4 = -136;
    private float[] heightOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        people = new GameObject[] { person1, person2, person3, person4, person5, person6, person7, person8, person9, person10 };      // alle GameObjects in einem Array
        heightOffset = new float[] { offset1, offset2, 18, offset3, offset4 };
        spawnPerson();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnPerson();
            timer = 0;
        }
    }

    void spawnPerson()
    {
        int randomIndex = Random.Range(0, people.Length);
        GameObject selectedPerson = people[randomIndex];        // zufällig ein GameObject wählen

        int randomHeight = Random.Range(0, heightOffset.Length);
        float selectedPosition = heightOffset[randomHeight];

        Instantiate(selectedPerson, new Vector3(transform.position.x, 0, selectedPosition), transform.rotation);
    }
}

using UnityEngine;

public class Evolution : MonoBehaviour
{
    public int numbercars;
    public GameObject[] cars;
    void Start()
    {
        cars = new GameObject[numbercars];
        foreach (GameObject car in cars) { 
            car.GetComponent<AI>().Randomize();
        }
    }
    public void NewGeneration()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class Evolution : MonoBehaviour
{
    public int numbercars;
    public GameObject[] cars;
    public GameObject[] layers;
    public float time = 20f;
    public GameObject bestCar;
    void Start()
    {
        layers = GameObject.FindGameObjectsWithTag("Layer");
        foreach (GameObject layer in layers)
        {
            layer.GetComponent<Layer>().Generate();
        }
        cars = new GameObject[numbercars];
        cars = GameObject.FindGameObjectsWithTag("Car");
        for (int i = 0; i < numbercars; i++)
        {
            if (i == 0)
            {
                GameObject b =Instantiate(bestCar, new Vector3(-12, 0), Quaternion.identity);
                b.transform.Rotate(new Vector3(0, 0, 180));
            }
            else
            {
                GameObject a = Instantiate(bestCar, new Vector3(-12, 0), Quaternion.identity);
                a.GetComponent<AI>().Randomize();
                a.transform.Rotate(new Vector3(0, 0, 180));
            }
        }
    }
    public void NewGeneration()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            time = 20f;
            layers = GameObject.FindGameObjectsWithTag("Layer");
            foreach (GameObject layer in layers)
            {
                layer.GetComponent<Layer>().Generate();
            }
            cars = new GameObject[numbercars];
            cars = GameObject.FindGameObjectsWithTag("Car");
            float max = -30f;
            foreach (GameObject car in cars)
            {
                if(car.transform.position.x > max)
                {
                    bestCar = car;
                }
                Destroy(car);
            }
            for (int i = 0; i < numbercars; i++) {
                if (i == 0)
                {
                    GameObject b = Instantiate(bestCar, new Vector3(-12, 0), Quaternion.identity);
                    b.transform.Rotate(new Vector3(0, 0, 180));
                }
                else {
                    GameObject a = Instantiate(bestCar, new Vector3(-12, 0), Quaternion.identity);
                    a.GetComponent<AI>().Mutate(bestCar.GetComponent<AI>());
                    a.transform.Rotate(new Vector3(0, 0, 180));
                }
            }
        }
    }
}

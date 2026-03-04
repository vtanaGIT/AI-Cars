using UnityEngine;
using UnityEngine.InputSystem.Android;

public class Evolution : MonoBehaviour
{
    public int Minimum(GameObject[] gos)
    {
        int min =0;
        for (int i = 0; i < gos.Length; i++) {
            if (gos[i].transform.position.x < gos[min].transform.position.x)
            {
                min = i;
            }
        }
        return min;
    }
    public int Maximum(GameObject[] gos)
    {
        int max = 0;
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].transform.position.x > gos[max].transform.position.x)
            {
                max = i;
            }
        }
        return max;
    }
    public bool IsNull(GameObject[] gos)
    {
        for (int i = 0; i < gos.Length; i++) {
            if (gos[i] == null) {
                return true;
                
            }
        } 
        return false;
    }
    public bool IsInMassive(GameObject gos1, GameObject[] gos2)
    {
        for (int i = 0; i < gos2.Length; i++) {
            if (gos2[i] == gos1)
            {
                return true;
            }
        }
        return false;
    }
    public int numbercars;
    public GameObject[] cars;
    public GameObject[] layers;
    public float time = 20f;
    public GameObject[] bestCar;
    public int l = 7;
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
                GameObject b = Instantiate(bestCar[0], new Vector3(-12, 0), Quaternion.identity);
                b.transform.Rotate(new Vector3(0, 0, 180));
            }
            else
            {
                GameObject a = Instantiate(bestCar[0], new Vector3(-12, 0), Quaternion.identity);
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
            l--;
            time = 20f;
            if (l <= 0) {
                layers = GameObject.FindGameObjectsWithTag("Layer");
                foreach (GameObject layer in layers)
                {
                    layer.GetComponent<Layer>().Generate();
                }
                l = 7;
            }
            cars = new GameObject[numbercars];
            cars = GameObject.FindGameObjectsWithTag("Car");
            foreach (GameObject car in cars)
            {
                if (car.transform.position.x > Maximum(bestCar)||IsNull(bestCar))
                {
                    
                    car.transform.rotation = Quaternion.identity;
                    car.transform.Rotate(new Vector3(0, 0, 180));
                    if(car.transform.position.x > Minimum(bestCar))
                    {
                        car.transform.position = new Vector3(-12, 0);
                        bestCar[Minimum(bestCar)]=car;
                    }
                    else
                    {
                        car.transform.position = new Vector3(-12, 0);
                        for (int i = 0; i < bestCar.Length; i++)
                        {
                            if (bestCar[i] == null)
                            {
                                bestCar[i] = car;
                                break;
                            }
                        }
                    }
                    
                }
                else
                {
                    Destroy(car);
                }
            }

            for (int i = 0; i < numbercars; i++) {

                int index = System.Array.IndexOf(bestCar, cars[i]);
                if (index != -1)
                {
                    GameObject b = Instantiate(bestCar[index]);
                    b.GetComponent<BoxCollider2D>().enabled = true;
                    b.GetComponent<AI>().enabled = true;
                    b.GetComponent<Car>().enabled = true;
                    bestCar[index] = b;
                    Destroy(cars[i]);
                }
                else {

                    int j = Random.Range(0,bestCar.Length);
                    GameObject a = Instantiate(bestCar[j]);
                    a.GetComponent<BoxCollider2D>().enabled = true;
                    a.GetComponent<AI>().enabled = true;
                    a.GetComponent<Car>().enabled = true;
                    a.GetComponent<AI>().Mutate(bestCar[j].GetComponent<AI>());
                }
            }
            
        }
    }
}

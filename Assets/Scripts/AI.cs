using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;

public class AI : MonoBehaviour
{
    float[] n1 = new float[25];
    float[] n2 = new float[17];
    float[] n3 = new float[10];
    float[] r = new float[4];
    float[,] w1 = new float[25, 17];
    float[,] w2 = new float[17, 10];
    float[,] w3 = new float[10, 4];
    float[] b = new float[3];
    public GameObject Layer;
    public float[] Result()
    {
        n1[0] = transform.position.x;
        n1[1] = transform.position.y;
        n1[2] = GetComponent<Rigidbody2D>().linearVelocity.x;
        n1[3] = GetComponent<Rigidbody2D>().linearVelocity.y;
        n1[4] = GetComponent<Rigidbody2D>().angularVelocity;
        for (int i = 5; i < n1.Length; i++)
        {
            if (i % 2 == 1)
            {
                n1[i] = Layer.GetComponent<Layer>().kids[(i - 5)/2].transform.position.x;
            }
            else
            {
                n1[i] = Layer.GetComponent<Layer>().kids[(i - 6)/2].transform.position.y;
            }

        }
        for (int i = 0; i < n1.Length; i++)
        {
            for (int j = 0; j < n2.Length; j++)
            {
                n2[j] += n1[i] * w1[i, j];
                if (i == n1.Length - 1)
                {
                    n2[j] += b[0];
                }
            }
        }
        for (int i = 0; i < n2.Length; i++)
        {
            for (int j = 0; j < n3.Length; j++)
            {
                n3[j] += n2[i] * w2[i, j];
                if (i == n2.Length - 1)
                {
                    n3[j] += b[1];
                    n3[j] = Mathf.Clamp(n3[j], 0, 1);
                }
            }
        }
        for (int i = 0; i < n3.Length; i++)
        {
            for (int j = 0; j < r.Length; j++)
            {
                r[j] += n1[i] * w3[i, j];
                if (i == n3.Length - 1)
                {
                    r[j] += b[2];
                    r[j] = Mathf.Clamp(n3[j], 0, 1);
                }
            }
        }
        return r;
    }
    public void Randomize()
    {
        for (int i = 0; i < n1.Length; i++)
        {
            for (int j = 0; j < n2.Length; j++)
            {
                w1[i, j] = Random.Range(-2.5f, 2.5f);
            }
        }
        for (int i = 0; i < n2.Length; i++)
        {
            for (int j = 0; j < n3.Length; j++)
            {
                w2[i, j] = Random.Range(-2.5f, 2.5f);
            }
        }
        for (int i = 0; i < n3.Length; i++)
        {
            for (int j = 0; j < r.Length; j++)
            {
                w3[i, j] = Random.Range(-2.5f, 2.5f);
            }
        }
        for(int i = 0;i<b.Length; i++)
        {
            b[i] = Random.Range(-2.5f, 2.5f);
        }
    }
    public void Mutate(AI ai)
    {
        for (int i = 0; i < n1.Length; i++)
        {
            for (int j = 0; j < n2.Length; j++)
            {
                w1[i, j] = Random.Range(-2.5f, 2.5f) / 50 + ai.w1[i,j];
            }
        }
        for (int i = 0; i < n2.Length; i++)
        {
            for (int j = 0; j < n3.Length; j++)
            {
                w2[i, j] = Random.Range(-2.5f, 2.5f) / 50 + ai.w2[i, j];
            }
        }
        for (int i = 0; i < n3.Length; i++)
        {
            for (int j = 0; j < r.Length; j++)
            {
                w3[i, j] = Random.Range(-2.5f, 2.5f) / 50 + ai.w3[i, j];
            }
        }
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = Random.Range(-2.5f, 2.5f) / 50 + ai.b[i];
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Layer")
        {
            Layer = collision.gameObject;
        }
    }
}

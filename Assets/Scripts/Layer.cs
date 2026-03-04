using Unity.VectorGraphics;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public PhysicsMaterial2D[] mats;
    public Color[] colors = { Color.black, Color.brown, Color.red, Color.lightPink, Color.white };
    public int count=10;
    public GameObject boxpr;
    public GameObject[] kids= new GameObject[10];
    public void Generate()
    {
        foreach(GameObject go in kids)
        {
            Destroy(go);
        }
        kids = new GameObject[10];
        for (int i = 0; i < count; i++)
        {
            int a = Random.Range(0,mats.Length);
            Vector2 pos = transform.position;
            float scx = transform.localScale.x/2;
            float scy = transform.localScale.y/2;
            float x = Random.Range(pos.x - scx, pos.x + scx);
            float y = Random.Range(pos.y - scy, pos.y + scy);
            GameObject box =Instantiate(boxpr, new Vector3(x, y), Quaternion.identity, gameObject.transform);
            box.transform.localScale =new Vector3(box.transform.localScale.x / transform.localScale.x, box.transform.localScale.y / transform.localScale.y, box.transform.localScale.z / transform.localScale.z);
            box.GetComponent<Collider2D>().sharedMaterial = mats[a];
            box.GetComponent<SpriteRenderer>().color = colors[a];
            kids[i] = box;
        }
    }
    private void Start()
    {
    }
}

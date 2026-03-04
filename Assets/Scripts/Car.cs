using System;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;
    public float AngSpeed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float velocity = Input.GetAxis("Vertical") * speed;
        float AngVelocity = Input.GetAxis("Horizontal") * AngSpeed;
        rb.AddRelativeForce(new Vector2(-velocity, 0),ForceMode2D.Impulse);
        rb.AddTorque(-AngVelocity);
    }
}

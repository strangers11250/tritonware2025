using UnityEngine;
public class movement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            rb.linearVelocityY = speed;
        }
        else if (Input.GetKey("s"))
        {
            rb.linearVelocityY = -speed;
        }
        else
        {
            rb.linearVelocityY = 0;
        }

        if (Input.GetKey("a"))
        {
            rb.linearVelocityX = -speed;
        }
        else if (Input.GetKey("d"))
        {
            rb.linearVelocityX = speed;
        }
        else
        {
            rb.linearVelocityX = 0;
        }

    }
}

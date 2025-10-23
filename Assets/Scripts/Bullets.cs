using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<basicAttacks>().TakeDamage(10);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optional: deal damage here
            Destroy(gameObject);
        }
    }
}
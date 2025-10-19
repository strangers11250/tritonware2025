using UnityEngine;

public class ParryTrigger : MonoBehaviour
{
    public string targetTag = "Enemy"; // Set this in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Hitbox triggered with: " + other.name);
            // You can call methods on parent or apply damage
            transform.root.GetComponent<basicAttacks>()?.OnHitEnemy(other.gameObject);
        }
    }
}

using System.Security.Cryptography;
using UnityEngine;

public class basicAttacks : MonoBehaviour
{
    public CircleCollider2D parryCollider;
    public CircleCollider2D attackCollider;
    public GameObject attackEffect;
    public GameObject parryEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z))
        {
            PerformAttack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformParry();
        }
    }

    void PerformAttack()
    {
        // Enable the attack collider and effect
        attackCollider.enabled = true;
        GameObject effect = Instantiate(attackEffect, transform.position, Quaternion.identity);
        effect.transform.parent = this.transform; // Make effect a child of the player
        Destroy(effect, 0.5f); // Destroy effect after 0.5 seconds

        // Disable after a short delay
        Invoke("EndAttack", 0.5f); // Attack lasts for 0.3 seconds
    }

    void EndAttack()
    {
        attackCollider.enabled = false;
    }

    void PerformParry()
    {
        // Enable the parry collider
        parryCollider.enabled = true;
        GameObject effect = Instantiate(parryEffect, transform.position, Quaternion.identity);
        effect.transform.parent = this.transform; // Make effect a child of the player
        Destroy(effect, 0.5f); // Destroy effect after 0.5 seconds

        // Disable after a short delay
        Invoke("EndParry", 0.5f); // Parry lasts for 0.5 seconds
    }

    void EndParry()
    {
        parryCollider.enabled = false;
    }
}

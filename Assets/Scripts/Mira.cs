using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mira : Enemy
{
    [Header("AI Settings")]
    public GameObject player;
    public float thinkInterval = 0.3f;
    private bool isUsingSkill = false;
    public float maxSpeedMultiplier = 1.8f;
    public float minSpeedMultiplier = 0.5f;
    public int baseMoveSpeed = 5;
    public int idealDistance = 5;
    public Rigidbody2D rb;

    [Header("Combat")]
    public int contactDamage = 30;
    public float contactCooldown = 1f;
    public float knockbackForce = 10f;
    private float lastContactTime = -999f;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(AIThinkLoop());
    }

    void Update()
    {
        Vector2 dist = new Vector2 (Mathf.Abs(transform.position.x - player.transform.position.x), Mathf.Abs(transform.position.y - player.transform.position.y));

        float distXMultiplier = Mathf.Clamp((dist.x - idealDistance), minSpeedMultiplier, maxSpeedMultiplier);
        float distYMultiplier = Mathf.Clamp(dist.y, 0, maxSpeedMultiplier);


        // --- Move horizontally toward player ---
        rb.linearVelocity = new Vector2(
            baseMoveSpeed * distXMultiplier * Mathf.Sign(player.transform.position.x - transform.position.x),
            baseMoveSpeed * distYMultiplier * Mathf.Sign(player.transform.position.y - transform.position.y)
        );
    }

    private IEnumerator AIThinkLoop()
    {
        while (true)
        {
            if (isAlive && player != null && !isUsingSkill)
            {
                ThinkAndAct();
            }
            yield return new WaitForSeconds(thinkInterval);
        }
    }

    private void ThinkAndAct()
    {
        

        // In range — choose a skill
        UseBestSkill();
    }
    private void UseBestSkill()
    {
        /*
        // Get usable skills (off cooldown)
        var usable = skillSet.Where(s => s != null && s.IsReady()).ToList();
        if (usable.Count == 0) return;

        // Choose skill with highest priority, or random if tied
        int maxPriority = usable.Max(s => s.priority);
        var bestSkills = usable.Where(s => s.priority == maxPriority).ToList();
        Skill chosen = bestSkills[Random.Range(0, bestSkills.Count)];

        StartCoroutine(UseSkill(chosen));
        */
    }

    protected override IEnumerator UseSkill(Skill skill)
    {
        isUsingSkill = true;

        Debug.Log(enemyName + " uses " + skill.skillName);
        skill.MarkUsed();

        foreach (var action in skill.actions)
        {
            yield return new WaitForSeconds(action.delayBefore);

            if (action.attackPrefab != null)
            {
                Vector3 spawnPos = transform.position + transform.forward * 0.5f;
                Instantiate(action.attackPrefab, spawnPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(action.duration);
        }

        isUsingSkill = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        // Handle collisions if needed
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject obj = collision.gameObject;
            if (Time.time - lastContactTime < contactCooldown) return;

            basicAttacks playerHealth = obj.GetComponent<basicAttacks>();

            if (playerHealth != null) { playerHealth.TakeDamage(30); }

            lastContactTime = Time.time;
            Debug.Log(enemyName + " collided and damaged player!");

            // --- Knockback effect ---
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Direction = from enemy → player
                Vector2 knockDir = (player.transform.position - transform.position).normalized;

                playerRb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
            }

        }
    }
}

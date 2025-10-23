using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Mira : Enemy
{
    [Header("AI Settings")]
    public GameObject player;
    public float thinkInterval = 0.5f;
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
    private bool moving = true;

    protected override void Start()
    {
        Debug.Log("Time: " + Time.time);
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(AIThinkLoop());
        foreach (var skill in skillSet)
        {
            skill.MarkUsed(); // So all skills are available at start
        }
    }

    void Update()
    {
        if (moving)
        {
            Vector2 dist = new Vector2(Mathf.Abs(transform.position.x - player.transform.position.x), Mathf.Abs(transform.position.y - player.transform.position.y));

            float distXMultiplier = Mathf.Clamp((dist.x - idealDistance) / 2f, minSpeedMultiplier, maxSpeedMultiplier);
            float distYMultiplier = Mathf.Clamp(dist.y, 0, maxSpeedMultiplier) / 3f;


            // --- Move horizontally toward player ---
            rb.linearVelocity = new Vector2(
                baseMoveSpeed * distXMultiplier * Mathf.Sign(player.transform.position.x - transform.position.x),
                baseMoveSpeed * distYMultiplier * Mathf.Sign(player.transform.position.y - transform.position.y)
            );
        }
    }

    private IEnumerator AIThinkLoop()
    {
        while (true)
        {
            if (isAlive && player != null && !isUsingSkill)
            {
                UseBestSkill();
            }
            yield return new WaitForSeconds(thinkInterval);
        }
    }

    private void UseBestSkill()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        // Get usable skills (off cooldown)
        var usable = skillSet.Where(s => s != null && s.IsReady() && s.range > dist).ToList();
        if (usable.Count == 0) return;

        // Choose skill with highest priority, or random if tied
        int maxPriority = usable.Max(s => s.priority);
        var bestSkills = usable.Where(s => s.priority == maxPriority).ToList();
        Skill chosen = bestSkills[Random.Range(0, bestSkills.Count)];

        StartCoroutine(UseSkill(chosen));
    }

    protected override IEnumerator UseSkill(Skill skill)
    {
        isUsingSkill = true;
        skill.MarkUsed();

        foreach (var action in skill.actions)
        {
            yield return new WaitForSeconds(action.delayBefore);

            if (action.attackPrefab != null)
            {
                GameObject actionObject = Instantiate(action.attackPrefab, this.transform);
                actionObject.transform.position = transform.position + action.offset;
                Vector2 dir = player.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                actionObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                if (action.isParryable)
                {
                    actionObject.GetComponent<SpriteRenderer>().color = Color.red; // Indicate parryable
                }
            }

            yield return new WaitForSeconds(action.duration);
        }

        isUsingSkill = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollisionWithPlayer(collision.gameObject);
    }
        

    void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollisionWithPlayer(collision.gameObject);
    }

    void HandleCollisionWithPlayer(GameObject obj)
    {
        // Handle collisions if needed
        if (obj.CompareTag("Player"))
        {
            if (Time.time - lastContactTime < contactCooldown) return;

            basicAttacks playerHealth = obj.GetComponent<basicAttacks>();

            if (playerHealth != null) { playerHealth.TakeDamage(30); }

            lastContactTime = Time.time;
            Debug.Log(enemyName + " collided and damaged player!");

        }
    }

}

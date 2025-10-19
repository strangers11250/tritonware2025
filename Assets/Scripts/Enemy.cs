using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a single enemy in the game
public class Enemy : MonoBehaviour
{
    [Header("Basic Info")]
    public string enemyName = "Undefined";
    public int maxHP = 100;
    private int currentHP;

    [Header("Skills")]
    public List<Skill> skillSet = new List<Skill>();

    [Header("References")]
    private bool isAlive = true;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        if (!isAlive) return;

        currentHP -= amount;
        Debug.Log(enemyName + " took " + amount + " damage. HP left: " + currentHP);

        if (currentHP <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        if (!isAlive) return;

        currentHP = Mathf.Min(currentHP + amount, maxHP);
        Debug.Log(enemyName + " healed to " + currentHP + "/" + maxHP);
    }

    void Die()
    {
        isAlive = false;
        Debug.Log(enemyName + " has died!");
        // Disable movement, play animation, destroy, etc.
        Destroy(gameObject, 2f);
    }

    // --- SKILL SYSTEM ---
    public void UseRandomSkill()
    {
        if (skillSet.Count == 0) return;

        int index = Random.Range(0, skillSet.Count);
        Skill chosenSkill = skillSet[index];
        StartCoroutine(UseSkill(chosenSkill));
    }

    IEnumerator UseSkill(Skill skill)
    {
        Debug.Log(enemyName + " uses skill: " + skill.skillName);

        foreach (var action in skill.actions)
        {
            yield return new WaitForSeconds(action.delayBefore);

            if (action.attackPrefab != null)
                Instantiate(action.attackPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(action.duration);
        }

        yield return new WaitForSeconds(skill.cooldown);
    }
}

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
    public bool isAlive = true;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    protected virtual void TakeDamage(int amount)
    {
        if (!isAlive) return;

        currentHP -= amount;
        Debug.Log(enemyName + " took " + amount + " damage. HP left: " + currentHP);

        if (currentHP <= 0)
            Die();
    }

    protected virtual void Heal(int amount)
    {
        if (!isAlive) return;

        currentHP = Mathf.Min(currentHP + amount, maxHP);
        Debug.Log(enemyName + " healed to " + currentHP + "/" + maxHP);
    }

    protected virtual void Die()
    {
        isAlive = false;
        Debug.Log(enemyName + " has died!");
        // Disable movement, play animation, destroy, etc.
        Destroy(gameObject, 2f);
    }

    // --- SKILL SYSTEM ---
    protected virtual void UseRandomSkill()
    {
        if (skillSet.Count == 0) return;

        int index = Random.Range(0, skillSet.Count);
        Skill chosenSkill = skillSet[index];
        StartCoroutine(UseSkill(chosenSkill));
    }

    protected virtual IEnumerator UseSkill(Skill skill)
    {
        Debug.Log(enemyName + " uses skill: " + skill.skillName);
        yield return new WaitForSeconds(skill.cooldown);
    }
}

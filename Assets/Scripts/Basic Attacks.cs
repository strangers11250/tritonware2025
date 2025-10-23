using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class basicAttacks : MonoBehaviour
{
    public GameObject attack;
    public GameObject parry;
    public float parryDuration = 0.5f;
    public int maxHP = 100;
    public int maxEnergy = 100;
    private int currentHP;
    private int currentEnergy;
    public Slider healthBarUI;
    public Slider energyBarUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
        if (healthBarUI != null)
            healthBarUI.maxValue = maxHP;
            UpdateHealthUI();
        if (energyBarUI != null)
            energyBarUI.maxValue = maxEnergy;
            UpdateEnergyUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnergy == maxEnergy)
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
        GameObject attacking = Instantiate(attack, this.transform);
        Destroy(attacking, 0.5f); // Attack effect lasts 0.5 seconds
        currentEnergy = 0;
        UpdateEnergyUI();
    }
    void PerformParry()
    {
        GameObject parrying = Instantiate(parry, this.transform);
        Destroy(parrying, parryDuration); // Parry effect lasts 0.5 seconds
    }

    public void OnHitEnemy(GameObject enemy)
    {
        Debug.Log("Player hit enemy: " + enemy.name);
        // Example: deal damage
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentEnergy += damage * 2; // Regain energy on taking damage
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHealthUI();
        UpdateEnergyUI();
        if (currentHP <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthBarUI.value = currentHP;
    }

    void UpdateEnergyUI()
    {
        energyBarUI.value = currentEnergy;
    }

    void Die()
    {
        Debug.Log("Player Died!");
    }
}

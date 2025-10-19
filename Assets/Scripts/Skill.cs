using UnityEngine;
using System.Collections;

[System.Serializable]
public class Skill
{
    public string skillName;
    public float cooldown = 2f;
    public AttackAction[] actions; // the steps or attacks in this skill
}

[System.Serializable]
public class AttackAction
{
    public string actionName;
    public float delayBefore;   // wait before performing
    public float duration;      // e.g., active hitbox time
    public GameObject attackPrefab;  // projectile or hitbox prefab
}
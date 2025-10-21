using UnityEngine;
using System.Collections;

[System.Serializable]
public class Skill : ScriptableObject
{
    public string skillName;
    public float cooldown = 2f;
    public float range = 3f;
    public int priority = 1;
    public AttackAction[] actions; // the steps or attacks in this skill

    [HideInInspector] public float lastUsedTime = -999f;

    public bool IsReady() => Time.time >= lastUsedTime + cooldown;
    public void MarkUsed() => lastUsedTime = Time.time;
}

[System.Serializable]
public class AttackAction
{
    public string actionName;
    public float delayBefore;   // wait before performing
    public float duration;      // e.g., active hitbox time
    public bool isParryable;
    public GameObject attackPrefab;  // projectile or hitbox prefab
}
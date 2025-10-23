using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Game/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public float cooldown = 2f;
    public float range = 3f;
    public int priority = 1;
    public AttackAction[] actions; // the steps or attacks in this skill

    [HideInInspector] public float lastUsedTime = 1;

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
    public Vector3 offset;         // position offset from user
}
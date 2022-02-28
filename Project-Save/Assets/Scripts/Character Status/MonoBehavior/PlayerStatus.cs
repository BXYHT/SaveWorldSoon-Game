using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("生命值")]
    public float maxHealth;
    public float currentHealth;

    [Header("伤害")]
    public int damage;
    public int damageFactor;
    private int baseDamageFactor;

    [Header("冲刺")]
    public int maxDashCount;
    public float dashTime;
    public float dashCDTime;
    public float dashSpeed;
    public bool isDashing;

    [Header("技能")]
    public bool increaseDamageAfterDash;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDashing = false;
        baseDamageFactor = 1;
        damageFactor = baseDamageFactor;
    }

    public void ResetDamageFactor()
    {
        damageFactor = baseDamageFactor;
    }
}

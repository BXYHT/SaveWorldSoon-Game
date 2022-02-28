using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("����ֵ")]
    public float maxHealth;
    public float currentHealth;

    [Header("�˺�")]
    public int damage;
    public int damageFactor;
    private int baseDamageFactor;

    [Header("���")]
    public int maxDashCount;
    public float dashTime;
    public float dashCDTime;
    public float dashSpeed;
    public bool isDashing;

    [Header("����")]
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

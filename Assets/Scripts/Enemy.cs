using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float currentHealth, maxHealth = 50f;
    [SerializeField] private EnemyHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

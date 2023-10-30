using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float currentHealth, maxHealth = 50f;
    [SerializeField] private EnemyHealthBar healthBar;
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private GameObject player;
    private const string IS_PLAYER = "Player";

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(IS_PLAYER);
    }

    private void Update()
    {
        enemy.SetDestination(player.transform.position);
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

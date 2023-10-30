using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float currentHealth, maxHealth = 50f;
    private EnemyHealthBar healthBar;
    private NavMeshAgent enemy;
    private GameObject player;
    private GameManager gameManager;
    private const string IS_PLAYER = "Player";

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        gameManager.KillCounterUpdate();
        Destroy(gameObject);
    }
}

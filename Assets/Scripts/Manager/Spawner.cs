using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnStartDelay = 2f;
    [SerializeField] private float spawnRate = 4f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnStartDelay, spawnRate);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }


}

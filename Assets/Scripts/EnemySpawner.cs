using System.Collections;
using UnityEngine;

// This is a monster spawner script, it behaves like the mob spawner in Minecraft - Gatsby

// Script was taken from this video: https://www.youtube.com/watch?v=pQajI2xHe5U  Credits go to Gatsby on YouTube.

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;                // Enemy prefab to spawn


    public int minEnemiesPerBatch = 2;            // Minimum number of enemies per batch
    public int maxEnemiesPerBatch = 4;            // Maximum number of enemies per batch


    public float minSpawnInterval = 3f;           // Minimum interval between spawns
    public float maxSpawnInterval = 5f;           // Maximum interval between spawns

    public float spawnBatchRadius = 20f;           // Radius within which enemies will be spawned in each batch

    public float spawnHeightOffset = 0f;

    public void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);

            int enemiesToSpawn = Random.Range(minEnemiesPerBatch, maxEnemiesPerBatch + 1);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector3 spawnOffset = Random.insideUnitCircle * spawnBatchRadius;
                spawnOffset.y = 0f;
                spawnOffset = spawnOffset.normalized * Random.Range(0f, spawnBatchRadius);
                Vector3 spawnPosition = transform.position + spawnOffset + new Vector3(0, spawnHeightOffset, 0);

                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }

        
    }
}



using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<EnemyData> enemies = new List<EnemyData>(); // List of enemy types and data
    public int currWave;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLocations; // Locations where enemies will spawn
    public int spawnIndex;

    public int waveDuration = 30; // Default duration for the wave
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        GenerateWave();
    }

    void FixedUpdate()
    {
        if (waveTimer > 0)
        {
            waveTimer -= Time.fixedDeltaTime;

            if (spawnTimer <= 0)
            {
                if (enemiesToSpawn.Count > 0)
                {
                    if (spawnLocations.Length > 0)
                    {
                        if (spawnIndex >= 0 && spawnIndex < spawnLocations.Length)
                        {
                            GameObject enemy = Instantiate(enemiesToSpawn[0], spawnLocations[spawnIndex].position, Quaternion.identity);
                            enemiesToSpawn.RemoveAt(0);
                            spawnedEnemies.Add(enemy);
                            spawnTimer = spawnInterval;

                            spawnIndex = (spawnIndex + 1) % spawnLocations.Length; // Cycle through spawn locations
                        }
                        else
                        {
                            Debug.LogError("Spawn index is out of bounds: " + spawnIndex);
                        }
                    }
                    else
                    {
                        Debug.LogError("Spawn locations array is empty.");
                    }
                }
                else
                {
                    waveTimer = 0; // If no enemies remain, end wave
                }
            }
            else
            {
                spawnTimer -= Time.fixedDeltaTime;
            }
        }
        else if (waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currWave++;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();

        spawnInterval = enemiesToSpawn.Count > 0 ? waveDuration / enemiesToSpawn.Count : waveDuration; // Prevent division by zero
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();

        while (waveValue > 0 && generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue >= randEnemyCost)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else
            {
                break;
            }
        }

        enemiesToSpawn = new List<GameObject>(generatedEnemies);
    }
}

[System.Serializable]
public class EnemyData
{
    public GameObject enemyPrefab;
    public int cost;
}

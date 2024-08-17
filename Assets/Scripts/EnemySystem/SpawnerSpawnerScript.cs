using System;
using Unity.Collections;
using UnityEngine;

public class SpawnerSpawnerScript : MonoBehaviour
{
    public GameObject spawnerPrefab;
    // public GameObject[] enemyPrefabs;
    public Rect spawnArea; // spawners are spawned on the edge of the spawnArea
    public GameObject enemyTarget;
    
    private float timeElapsed = 0f;
    private float spawnDelay = 0f;
    
    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime * 10f;
        spawnDelay -= Time.fixedDeltaTime;
        if(spawnDelay <= 0f) Spawn();
    }
    
    private void Spawn()
    {
        // the most magic numbers I have ever written in one function // comment about random looking formulas // comment about comment
        float nextDelay = UnityEngine.Random.Range(Mathf.Max(10f - 0.02f * timeElapsed, 3.0f), Mathf.Max(8f - 0.02f * timeElapsed, 2.0f));
        float spawnerCountAvg = 1f + Mathf.Pow(timeElapsed, 0.75f) / 50f;
        int spawnerCount = (int) spawnerCountAvg + (UnityEngine.Random.Range(0f, 1f) < (spawnerCountAvg % 1f) ? 1 : 0);
        
        spawnDelay += nextDelay;
        for(int i = 0; i < spawnerCount; i++) {
            // int enemyCount = UnityEngine.Random.Range((int) (2f + Mathf.Pow(timeElapsed + 10f, 0.625f) / 4f), (int) (3.6f + Mathf.Pow(timeElapsed + 10f, 0.75f) / 4f));
            float enemyDelay = UnityEngine.Random.Range(Mathf.Max(1f - 0.005f * timeElapsed, 0.2f), Mathf.Max(2f - 0.01f * timeElapsed, 1f));
            // int enemyType = UnityEngine.Random.Range(0, Mathf.Min((int) (1f + timeElapsed / 30f), enemyPrefabs.Length));
            int[] enemyCounts = {
                randomEnemyCount(-1f, 0.5f, -0.3f, 0.5f),
                randomEnemyCount(3f, 0.75f, 5f, 0.625f),
                randomEnemyCount(-7.6f, 0.7f, -5.3f, 0.7f)
            };
            
            GameObject spawner = Instantiate(spawnerPrefab);
            spawner.transform.position = randomSpawnerPosition();
            SpawnerScript spawnerScript = spawner.GetComponent<SpawnerScript>();
            // spawnerScript.spawnCount = enemyCount;
            spawnerScript.spawnCounts = enemyCounts;
            spawnerScript.spawnDelay = enemyDelay;
            // spawnerScript.enemyPrefab = enemyPrefabs[enemyType];
            spawnerScript.enemyTarget = enemyTarget;
        }
    }
    
    private int randomEnemyCount(float startMin, float expMin, float startMax, float expMax) {
        return UnityEngine.Random.Range(
            Mathf.Max(0, (int) (startMin + (Mathf.Pow(timeElapsed + 10f, expMin) - Mathf.Pow(10f, expMin)) / 4f)),
            Mathf.Max(1, (int) (startMax + (Mathf.Pow(timeElapsed + 10f, expMax) - Mathf.Pow(10f, expMax)) / 4f))
        );
    }
    
    private Vector2 randomSpawnerPosition()
    {
        float fac = UnityEngine.Random.Range(0f, 2f * spawnArea.width + 2f * spawnArea.height);
        if(fac < spawnArea.width) return new Vector2(spawnArea.x + fac, spawnArea.y);
        if(fac < spawnArea.width + spawnArea.height) return new Vector2(spawnArea.x + spawnArea.width, spawnArea.y + (fac - spawnArea.width));
        if(fac < 2f * spawnArea.width + spawnArea.height) return new Vector2(spawnArea.x + spawnArea.width - (fac - spawnArea.width - spawnArea.height), spawnArea.y + spawnArea.height);
        return new Vector2(spawnArea.x, spawnArea.y + spawnArea.height - (fac - 2f * spawnArea.width - spawnArea.height));
    }
}

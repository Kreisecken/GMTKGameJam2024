using System;
using Unity.Collections;
using UnityEngine;

public class SpawnerSpawnerScript : MonoBehaviour
{
    public GameObject spawnerPrefab;
    // public GameObject[] enemyPrefabs;
    public Rect spawnArea; // spawners are spawned on the edge of the spawnArea
    public GameObject enemyTarget;
    private float waveDuration = 30f;
    public GameStateManager gameStateManager;
    public float waveEndTime = 5f;
    
    private bool running = false; // true if a wave is currently active
    private int currentWave = 1;
    private float timeElapsed = 0f; // total time elapsed in all rounds so far
    private float spawnDelay = 0f;
    private float remainingWaveEndTime = 0f;
    
    void Start()
    {
        gameStateManager.SetCurrentWave(currentWave);
    }
    
    void FixedUpdate()
    {
        if(remainingWaveEndTime > 0f) remainingWaveEndTime -= Time.fixedDeltaTime;
        gameStateManager.SetWaveRunning(running || remainingWaveEndTime > 0f);
        
        if(running) {
            timeElapsed += Time.fixedDeltaTime;
            
            spawnDelay -= Time.fixedDeltaTime;
            if(spawnDelay <= 0f) Spawn();
            
            if(timeElapsed >= currentWave * waveDuration) {
                running = false;
                remainingWaveEndTime = waveEndTime;
                currentWave++;
                gameStateManager.SetCurrentWave(currentWave);
            }
        }
    }
    
    public void StartRound() {
        if(!running) {
            running = true;
        }
    }
    
    private void Spawn()
    {
        // expressions for different enemy stats that are slightly random and generally get more difficult as time (timeElapsed) goes on
        float nextDelay = UnityEngine.Random.Range(Mathf.Max(10f - 0.0075f * timeElapsed, 3.0f), Mathf.Max(8f - 0.0075f * timeElapsed, 2.0f));
        float spawnerCountAvg = 1f + Mathf.Pow(timeElapsed, 0.75f) / 50f;
        int spawnerCount = (int) spawnerCountAvg + (UnityEngine.Random.Range(0f, 1f) < (spawnerCountAvg % 1f) ? 1 : 0);
        
        spawnDelay += nextDelay;
        for(int i = 0; i < spawnerCount; i++) {
            // int enemyCount = UnityEngine.Random.Range((int) (2f + Mathf.Pow(timeElapsed + 10f, 0.625f) / 4f), (int) (3.6f + Mathf.Pow(timeElapsed + 10f, 0.75f) / 4f));
            float enemyDelay = UnityEngine.Random.Range(Mathf.Max(1f - 0.005f * timeElapsed, 0.2f), Mathf.Max(2f - 0.01f * timeElapsed, 1f));
            // int enemyType = UnityEngine.Random.Range(0, Mathf.Min((int) (1f + timeElapsed / 30f), enemyPrefabs.Length));
            // int[] enemyCounts = {
            //     randomEnemyCount(-1f, 0.5f, -0.3f, 0.5f),
            //     randomEnemyCount(3f, 0.75f, 5f, 0.625f),
            //     randomEnemyCount(-7.6f, 0.7f, -5.3f, 0.7f)
            // };
            // int[] enemyCounts = { // TODO: adjust values
            //     randomEnemyCount(0f, 0.5f, 1f, 0.5f),
            //     randomEnemyCount(0f, 0.5f, 1f, 0.5f),
            //     randomEnemyCount(0f, 0.5f, 1f, 0.5f),
            //     randomEnemyCount(0f, 0.5f, 1f, 0.5f),
            //     randomEnemyCount(0f, 0.5f, 1f, 0.5f),
            //     randomEnemyCount(0f, 0.5f, 1f, 0.5f),
            //     randomEnemyCount(0f, 0.5f, 1f, 0.5f)
            // };
            int[] enemyCounts = {
                randomEnemyCount(-1f, 0.5f, -0.3f, 0.5f), // Tank
                randomEnemyCount(3f, 0.75f, 5f, 0.625f), // Enemy
                randomEnemyCount(-1f, 0.4f, -0.3f, 0.4f), // Summoner
                randomEnemyCount(-7.6f, 0.6f, -5.3f, 0.6f), // Healer
                randomEnemyCount(-2f, 0.5f, -0.3f, 0.5f), // Range
                randomEnemyCount(-1f, 0.3f, -0.7f, 0.3f), // Boss
            };
            
            // create spawner
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
        return (int) UnityEngine.Random.Range(
            Mathf.Max(0f, startMin + (Mathf.Pow(timeElapsed + 10f, expMin) - Mathf.Pow(10f, expMin)) / 4f),
            Mathf.Max(1f, startMax + (Mathf.Pow(timeElapsed + 10f, expMax) - Mathf.Pow(10f, expMax)) / 4f)
        );
    }
    
    private Vector2 randomSpawnerPosition() // gets a random position on the edge of spawnArea
    {
        float fac = UnityEngine.Random.Range(0f, 2f * spawnArea.width + 2f * spawnArea.height);
        if(fac < spawnArea.width) return new Vector2(spawnArea.x + fac, spawnArea.y);
        if(fac < spawnArea.width + spawnArea.height) return new Vector2(spawnArea.x + spawnArea.width, spawnArea.y + (fac - spawnArea.width));
        if(fac < 2f * spawnArea.width + spawnArea.height) return new Vector2(spawnArea.x + spawnArea.width - (fac - spawnArea.width - spawnArea.height), spawnArea.y + spawnArea.height);
        return new Vector2(spawnArea.x, spawnArea.y + spawnArea.height - (fac - 2f * spawnArea.width - spawnArea.height));
    }
}

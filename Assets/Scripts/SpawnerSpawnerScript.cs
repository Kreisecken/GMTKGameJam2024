using System;
using UnityEngine;

public class SpawnerSpawnerScript : MonoBehaviour
{
    public GameObject spawnerPrefab;
    public Rect spawnArea; // spawners are spawned on the edge of the spawnArea
    public GameObject enemyTarget;
    
    private float timeElapsed = 0f;
    private float spawnDelay = 0f;
    
    void Start()
    {
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        spawnDelay -= Time.deltaTime;
        if(spawnDelay <= 0f) Spawn();
    }
    
    private void Spawn() {
        float nextDelay = UnityEngine.Random.Range(Mathf.Max(10f - 0.02f * timeElapsed, 3.0f), Mathf.Max(8f - 0.02f * timeElapsed, 2.0f));
        float spawnerCountAvg = 1f + Mathf.Pow(timeElapsed, 0.75f) / 50f;
        int spawnerCount = (int) spawnerCountAvg + (UnityEngine.Random.Range(0f, 1f) < (spawnerCountAvg % 1f) ? 1 : 0);
        
        spawnDelay += nextDelay;
        for(int i = 0; i < spawnerCount; i++) {
            int enemyCount = UnityEngine.Random.Range((int) (2f + Mathf.Pow(timeElapsed + 10f, 0.625f) / 4f), (int) (3.6f + Mathf.Pow(timeElapsed + 10f, 0.75f) / 4f));
            float enemyDelay = UnityEngine.Random.Range(Mathf.Max(1f - 0.005f * timeElapsed, 0.2f), Mathf.Max(2f - 0.01f * timeElapsed, 1f));
            // TODO: enemy type
            
            GameObject spawner = Instantiate(spawnerPrefab);
            spawner.transform.position = randomSpawnerPosition();
            SpawnerScript spawnerScript = spawner.GetComponent<SpawnerScript>();
            spawnerScript.spawnCount = enemyCount;
            spawnerScript.spawnDelay = enemyDelay;
            spawnerScript.enemyTarget = enemyTarget;
        }
    }
    
    private Vector2 randomSpawnerPosition() {
        float fac = UnityEngine.Random.Range(0f, 2f * spawnArea.width + 2f * spawnArea.height);
        if(fac < spawnArea.width) return new Vector2(spawnArea.x + fac, spawnArea.y);
        if(fac < spawnArea.width + spawnArea.height) return new Vector2(spawnArea.x + spawnArea.width, spawnArea.y + (fac - spawnArea.width));
        if(fac < 2f * spawnArea.width + spawnArea.height) return new Vector2(spawnArea.x + spawnArea.width - (fac - spawnArea.width - spawnArea.height), spawnArea.y + spawnArea.height);
        return new Vector2(spawnArea.x, spawnArea.y + spawnArea.height - (fac - 2f * spawnArea.width - spawnArea.height));
    }
}

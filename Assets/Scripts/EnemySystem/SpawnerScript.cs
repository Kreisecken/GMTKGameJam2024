using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // public GameObject enemyPrefab;
    public GameObject[] enemyPrefabs;
    // public int spawnCount = 5;
    public int[] spawnCounts = { 0, 5, 0 };
    public float spawnDelay = 1f;
    public float startDelay = 5f;
    public GameObject enemyTarget;
    
    private float spawnTimer = 0f;
    private int enemyIndex = 0;
    
    void Start()
    {
        while(spawnCounts[enemyIndex] <= 0 && enemyIndex < spawnCounts.Length - 1) enemyIndex++;
    }

    void FixedUpdate()
    {
        if(startDelay > 0f) {
            startDelay -= Time.fixedDeltaTime;
            return;
        }
        
        spawnTimer -= Time.fixedDeltaTime;
        if(spawnTimer <= 0f) {
            
            // GameObject spawn = Instantiate(enemyPrefab);
            GameObject spawn = Instantiate(enemyPrefabs[enemyIndex]);
            spawn.transform.position = transform.position;
            spawn.GetComponent<EnemyController>().target = enemyTarget;
            
            spawnTimer += spawnDelay;
            // spawnCount--;
            spawnCounts[enemyIndex]--;
            // if(spawnCount <= 0) Destroy(gameObject);
            while(spawnCounts[enemyIndex] <= 0 && enemyIndex < spawnCounts.Length - 1) enemyIndex++;
            if(spawnCounts[enemyIndex] <= 0 && enemyIndex >= spawnCounts.Length - 1) Destroy(gameObject);
        }
    }
}

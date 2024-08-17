using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int spawnCount = 5;
    public float spawnDelay = 1f;
    public float startDelay = 5f;
    public GameObject enemyTarget;
    
    private float spawnTimer = 0f;
    
    void Start()
    {
    }

    void FixedUpdate()
    {
        if(startDelay > 0f) {
            startDelay -= Time.fixedDeltaTime;
            return;
        }
        
        spawnTimer -= Time.fixedDeltaTime;
        if(spawnTimer <= 0f) {
            GameObject spawn = Instantiate(enemyPrefab);
            spawn.transform.position = transform.position;
            spawn.GetComponent<EnemyController>().target = enemyTarget;
            
            spawnTimer += spawnDelay;
            spawnCount--;
            if(spawnCount <= 0) Destroy(gameObject);
        }
    }
}

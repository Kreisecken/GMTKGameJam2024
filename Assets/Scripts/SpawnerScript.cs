using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int spawnCount = 5;
    public float spawnDelay = 1f;
    public float startDelay = 5f;
    public GameObject target;
    
    private float spawnTimer = 0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(startDelay > 0f) {
            startDelay -= Time.deltaTime;
            return;
        }
        
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0f) {
            GameObject spawn = Instantiate(enemyPrefab);
            spawn.transform.position = transform.position;
            spawn.GetComponent<EnemyController>().target = target;
            
            spawnTimer += spawnDelay;
            spawnCount--;
            if(spawnCount <= 0) Destroy(gameObject);
        }
    }
}

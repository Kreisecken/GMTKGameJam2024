using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{
    public float healHp = 5f;
    public float healRange = 3f;
    public float healDelay = 3f;
    
    private float healTimer = 0f;
    
    void Start()
    {
    }

    void FixedUpdate()
    {
        healTimer -= Time.fixedDeltaTime;
        if(healTimer <= 0f) {
            healTimer += healDelay;
            List<Enemy> enemyList = Enemy.GetEnemiesInRangeSorted(transform.position, healRange);
            foreach(Enemy enemy in enemyList) {
                if(!enemy.IsFullHp()) {
                    enemy.Heal(healHp);
                    break;
                }
            }
        }
    }
}

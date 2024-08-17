using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackDamage = 1f;
    public float attackRange = 1f;
    public float attackDelay = 1f;
    
    private float attackTimer = 0f;
    
    void FixedUpdate()
    {
        attackTimer -= Time.fixedDeltaTime;
        if(attackTimer <= 0f) {
            attackTimer += attackDelay;
            
            // attack nearest Tower
            Tower tower = Tower.ClosestTower(transform.position, attackRange);
            if(tower != null) tower.Damage(attackDamage);
        }
    }
}

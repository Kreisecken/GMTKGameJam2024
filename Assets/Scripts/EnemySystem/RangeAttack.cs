using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public float attackDamage = 1f;
    public float attackRange = 8f;
    public float attackDelay = 1f;
    public SimpleMovingProjectile projectilePrefab;
    
    private float attackTimer = 0f;
    
    void FixedUpdate()
    {
        attackTimer -= Time.fixedDeltaTime;
        if(attackTimer <= 0f) {
            attackTimer += attackDelay;
            
            // shoot projectile at nearest tower
            Tower tower = Tower.ClosestTower(transform.position, attackRange);
            if(tower != null) Projectile.CreateProjectile(projectilePrefab).FireProjectile(transform.position, (tower.transform.position - transform.position).normalized);
        }
    }
}

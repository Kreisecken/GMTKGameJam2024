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
        if(attackTimer <= 0f) 
        {
            attackTimer += attackDelay;

            if (Tower.TryGetClosestTower(transform.position, attackRange, out Tower tower)) 
            {
                tower.Damage(attackDamage);
            }
        }
    }
}

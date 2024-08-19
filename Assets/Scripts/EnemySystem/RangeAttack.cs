using Unity.VisualScripting;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public float attackDamage = 1f;
    public float attackRange = 8f;
    public float attackDelay = 1f;
    
    [Header("Projectile Properties")]
    public ProjectileSprite projectileSprite;
    public ForwardMovement  forwardMovement;
    public DamageOnHit      damageOnHit;
    public Pierce           pierce;
    public DecayOverTime    decayOverTime;
    
    private float attackTimer = 0f;
    
    void FixedUpdate()
    {
        attackTimer -= Time.fixedDeltaTime;
        if(attackTimer <= 0f)
        {
            attackTimer += attackDelay;
            if(Tower.TryGetClosestTower(transform.position, attackRange, out Tower tower))
            {
                // Projectile.CreateProjectile(projectilePrefab).FireProjectile(transform.position, (tower.transform.position - transform.position).normalized);
                ProjectileInstantiator.CreateProjectile(transform.position, (tower.transform.position - transform.position).normalized)
                    .InteractWithTowers()
                    .AddBehaviour(projectileSprite)
                    .AddBehaviour(forwardMovement)
                    .AddBehaviour(damageOnHit)
                    .AddBehaviour(pierce.Clone())
                    .AddBehaviour(decayOverTime.Clone())
                    .FireProjectile();
            }
        }
    }
}

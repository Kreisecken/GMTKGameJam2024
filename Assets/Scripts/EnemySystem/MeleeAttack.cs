using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackDamage = 1f;
    public float attackRange = 1f;
    public float attackDelay = 1f;
    public float lifeStealFactor = 0f;
    
    private float attackTimer = 0f;
    
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        attackTimer -= Time.fixedDeltaTime;
        if(attackTimer <= 0f) 
        {
            attackTimer += attackDelay;
            
            // attack nearest Tower
            if (Tower.TryGetClosestTower(transform.position, attackRange, out Tower tower)) 
            {
                animator.SetTrigger("AttackMelee");
                tower.Damage(attackDamage);
                if(lifeStealFactor != 0f) 
                {
                    GetComponent<Enemy>().Heal(attackDamage * lifeStealFactor); // TODO: don't heall the full attackDamage * lifeStealFactor when less damage is required to destroy the tower (not very important)
                }
            }
        }
    }
}

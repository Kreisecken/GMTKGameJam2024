using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleAttackTower : PlaceableTower
{
    [Header("Simple Attack Tower Properties")]


    public ProjectileSprite projectileSprite;
    public ForwardMovement  forwardMovement;
    public DamageOnHit      damageOnHit;
    public Pierce           pierce;
    public DecayOverTime    decayOverTime;
    public Homing           homing;

    public float range    = 1f;
    public float fireRate = 1f;

    private float internalFireRate;

    public void Update()
    {
        //rangeIndicator.transform.parent = null;
        rangeIndicator.transform.localScale = new Vector3(range, range, 1f);
        //rangeIndicator.transform.parent = gameObject.transform;

        if (!placed) return;

        internalFireRate -= Time.deltaTime;

        if (internalFireRate <= 0)
        {
            if (!Enemy.TryGetClosestEnemy(transform.position, range, out Enemy enemy)) return;

            Vector2 enemyPosition = enemy.transform.position;

            // Vector2 playerPosition = Player.Instance.transform.position;
            Debug.Log("Fire Projectile");
            // ProjectileInstantiator.CreateProjectile(transform.position, (playerPosition - (Vector2)transform.position).normalized)
            ProjectileInstantiator.CreateProjectile(transform.position, (enemyPosition - (Vector2)transform.position).normalized)
                // .InteractWithPlayer()
                .InteractWithEnemies()
                .AddBehaviour(projectileSprite)
                .AddBehaviour(forwardMovement)
                .AddBehaviour(damageOnHit)
                .AddBehaviour(decayOverTime.Clone())
                .AddBehaviour(pierce.Clone())
                // .AddBehaviour(homing.WithTarget(Player.Instance.gameObject))
            .FireProjectile();

            internalFireRate = fireRate;
        }
    }
}
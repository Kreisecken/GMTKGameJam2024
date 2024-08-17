using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleAttackTower : AttackTower
{
    public SimpleMovingProjectile projectilePrefab;

    public float fireRate = 1f;

    public new void Update()
    {
        if (fireRate < 0) return;

        fireRate -= Time.deltaTime;

        if (fireRate <= 0)
        {
            Vector2 enemyPosition = Enemy.ClosestEnemy(transform.position, towerStats.range).transform.position;
            CreateProjectile(projectilePrefab).FireProjectile((enemyPosition - (Vector2)transform.position).normalized);
            fireRate = 1f;
        }
    }
}
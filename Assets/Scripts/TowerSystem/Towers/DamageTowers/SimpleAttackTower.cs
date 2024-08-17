using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleAttackTower : AttackTower
{
    [Header("Simple Attack Tower Properties")]
    public SimpleMovingProjectile projectilePrefab;
    public float range = 1f;
    public GameObject rangeIndicator;
    public float fireRate = 1f;

    private float internalFireRate;

    public new void Update()
    {
        rangeIndicator.transform.localScale = new Vector3(range, range, 1f);
        if (!placed) return;

        //rangeIndicator.transform.parent = null;
        //rangeIndicator.transform.parent = gameObject.transform;

        internalFireRate -= Time.deltaTime;

        if (internalFireRate <= 0)
        {
            Enemy enemy = Enemy.ClosestEnemy(transform.position, range);

            if (enemy == null) return;

            Vector2 enemyPosition = enemy.transform.position;

            Projectile.CreateProjectile(projectilePrefab).FireProjectile(transform.position, (enemyPosition - (Vector2)transform.position).normalized);

            internalFireRate = fireRate;
        }
    }
}
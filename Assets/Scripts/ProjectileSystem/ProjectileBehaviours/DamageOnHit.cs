using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class DamageOnHit : ProjectileBehaviour
{
    public float damage = 1f;
    
    public override void OnHit(Projectile projectile, Player player)
    {
        if (projectile.DoesInteractWithPlayer)
            player.Damage(damage);
    }

    public override void OnHit(Projectile projectile, Tower tower)
    {
        if (projectile.DoesInteractWithTowers)
            tower.Damage(damage);
    }

    public override void OnHit(Projectile projectile, Enemy enemy)
    {
        if (projectile.DoesInteractWithEnemies)
            enemy.Damage(damage);
    }

    public override T Clone<T>()
        => new DamageOnHit { damage = damage } as T;
}
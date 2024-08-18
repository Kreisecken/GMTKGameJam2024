using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Pierce : ProjectileBehaviour
{
    public int pierce = 1;

    public override void Update(Projectile projectile)
    {
        if (pierce <= 0)
            projectile.Destroy();
    }

    public override void OnHit(Projectile projectile, Player player)
    {
        if (projectile.DoesInteractWithPlayer)
            pierce--;
    }

    public override void OnHit(Projectile projectile, Tower tower)
    {
        if (projectile.DoesInteractWithTowers)
            pierce--;
    }

    public override void OnHit(Projectile projectile, Enemy enemy)
    {
        if (projectile.DoesInteractWithEnemies)
            pierce--;
    }

    public override T Clone<T>()
        => new Pierce { pierce = pierce } as T;    
}
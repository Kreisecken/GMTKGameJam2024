using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class DecayOverTime : ProjectileBehaviour
{
    public float lifetime  = 5f;

    public override void Update(Projectile projectile)
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            projectile.Destroy();
        }
    }

    public override T Clone<T>()
        => new DecayOverTime { lifetime = lifetime } as T;
}
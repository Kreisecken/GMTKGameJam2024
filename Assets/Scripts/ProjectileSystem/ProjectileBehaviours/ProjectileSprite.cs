using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class ProjectileSprite : ProjectileBehaviour
{
    public Sprite sprite;

    public override void Start(Projectile projectile)
    {
        projectile.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public override T Clone<T>()
        => new ProjectileSprite { } as T;    
}
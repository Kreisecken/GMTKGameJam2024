using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class ProjectileSprite : ProjectileBehaviour
{
    public Sprite sprite;
    public Animator animator;

    public override void Start(Projectile projectile)
    {
        projectile.GetComponent<SpriteRenderer>().sprite = sprite;
        Animator anim = projectile.gameObject.AddComponent<Animator>();
        anim.runtimeAnimatorController = animator.runtimeAnimatorController;
    }

    public override T Clone<T>()
        => new ProjectileSprite { } as T;    
}
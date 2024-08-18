using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class ProjectileBehaviour
{
    public virtual void Start      (Projectile projectile) { }
    public virtual void Update     (Projectile projectile) { }
    public virtual void FixedUpdate(Projectile projectile) { }

    public virtual void OnHit(Projectile projectile, Player player) { }
    public virtual void OnHit(Projectile projectile, Enemy  enemy ) { }
    public virtual void OnHit(Projectile projectile, Tower  tower ) { }
    public virtual void OnHit(Projectile projectile, Collider2D collider) { }
    
    public void OnTriggerEnter2D(Projectile projectile, Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player)) OnHit(projectile, player);
        if (collider.gameObject.TryGetComponent(out Enemy enemy  )) OnHit(projectile, enemy );
        if (collider.gameObject.TryGetComponent(out Tower tower  )) OnHit(projectile, tower );

        if (player || enemy || tower) OnHit(projectile, collider);
    }

    public ProjectileBehaviour Clone() => Clone<ProjectileBehaviour>();
    public virtual T Clone<T>() where T : ProjectileBehaviour, new() => new();
}
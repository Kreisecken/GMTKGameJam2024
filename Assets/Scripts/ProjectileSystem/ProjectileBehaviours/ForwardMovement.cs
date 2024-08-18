using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class ForwardMovement : ProjectileBehaviour
{
    public Vector2 direction = Vector2.zero;
    public float   speed     = 5f;

    public override void FixedUpdate(Projectile projectile)
    {
        projectile.Rigidbody2D.linearVelocity = projectile.transform.up * speed;
    }

    public override T Clone<T>()
        => new ForwardMovement { direction = direction, speed = speed } as T;
}
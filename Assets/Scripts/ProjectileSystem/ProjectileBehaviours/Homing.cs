using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Homing : ProjectileBehaviour
{
    public GameObject target;
    public float angleSpeed = 180f;
    public float distance = 10f;

    public override void FixedUpdate(Projectile projectile)
    {
        if (target == null) return;

        if (Vector3.Distance(projectile.transform.position, target.transform.position) >= distance)
        {
            return;
        }

        Vector2 direction = (Vector2)target.transform.position - (Vector2)projectile.transform.position * UnityEngine.Random.Range(0.9f, 1.1f);
        float rotateAmount = Vector3.Cross (direction, projectile.transform.up).z;
        
        projectile.Rigidbody2D.angularVelocity = -rotateAmount * angleSpeed;
    }

    public Homing WithTarget(GameObject target)
    {
        if (this.target != target) 
        {
            Homing clone = Clone<Homing>();
            clone.target = target;
            return clone;
        }

        return this;
    }

    public override T Clone<T>()
        => new Homing { target = target } as T;
}
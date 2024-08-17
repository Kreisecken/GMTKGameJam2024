using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovingProjectile : Projectile
{
    public float speed = 5f;
    public Vector2 direction = Vector2.zero;

    public float damage = 1f;
    public float lifetime = 5f;

    public void Update()
    {
        if (lifetime < 0) return;

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime * direction);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void FireProjectile(Vector2 direction, float speedMultiplier = 1f)
    {
        this.direction = direction;
        speed *= speedMultiplier;

        FireProjectile();
    }
}
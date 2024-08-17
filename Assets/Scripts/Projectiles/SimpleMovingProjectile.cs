using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovingProjectile : Projectile
{
    public float speed = 5f;
    public Vector2 direction = Vector2.zero;

    public float damage = 1f;
    public float lifetime = 5f;

    public bool doDamageAgainstThePlayer = false;
    public bool doDamageAgainstEnemies = false;
    public bool doDamageAgainstTowers = false;

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

        if (doDamageAgainstThePlayer && collision.gameObject.TryGetComponent(out Player player))
        {
            // player.Damage(damage);
        }

        if (doDamageAgainstEnemies && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            // enemy.Damage(damage);
        }

        if (doDamageAgainstTowers && collision.gameObject.TryGetComponent(out Tower tower))
        {
            // tower.Damage(damage);
        }
    }

    public void FireProjectile(Vector2 position, Vector2 direction, float speedMultiplier = 1f)
    {
        this.transform.position = position;
        this.direction = direction;
        speed *= speedMultiplier;

        FireProjectile();
    }
}
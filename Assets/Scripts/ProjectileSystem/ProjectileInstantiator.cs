using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileInstantiator : MonoBehaviour
{
    public static ProjectileInstantiator Instance { get; private set; }

    public Projectile projectilePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Debug.LogWarning("Multiple ProjectileInstantiator instances detected. Destroying the new one.");
        Destroy(gameObject);
    }

    public static Projectile CreateProjectile(Vector3 position, Vector2 direction)
    {
        Projectile projectile = Instantiate(Instance.projectilePrefab.gameObject).GetComponent<Projectile>();

        projectile.transform.position = position;
        projectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        return projectile;
    }
}
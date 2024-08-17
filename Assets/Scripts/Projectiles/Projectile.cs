using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    public bool fired = false;

    protected virtual void FireProjectile()
    {
        fired = true;
    }

    public static T CreateProjectile<T>(T projectilePrefab) where T : Projectile
    {
        T projectile = Instantiate(projectilePrefab.gameObject).GetComponent<T>();
        projectile.gameObject.SetActive(true);

        return projectile;
    }
}
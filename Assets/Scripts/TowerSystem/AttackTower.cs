using UnityEngine;
using UnityEngine.InputSystem;

public class AttackTower : Tower
{
    public T CreateProjectile<T>(T projectilePrefab) where T : Projectile
    {
        T projectile = Instantiate(projectilePrefab.gameObject).GetComponent<T>();
        projectile.gameObject.SetActive(true);

        return projectile;
    }
}
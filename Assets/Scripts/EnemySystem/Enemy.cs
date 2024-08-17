using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> Enemies { get; private set; } = new();

    public void Awake()
    {
        Enemies.Add(this);
    }

    public void OnDestroy()
    {
        Enemies.Remove(this);
    }

    public static List<Enemy> GetEnemiesInRange(Vector3 position, float range)
    {
        List<Enemy> enemiesInRange = new();

        foreach (var enemy in Enemies)
        {
            if (Vector3.Distance(position, enemy.transform.position) <= range)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange;
    }

    public static List<Enemy> GetEnemiesInRangeSorted(Vector3 position, float range)
    {
        var enemies = GetEnemiesInRange(position, range);
        enemies.Sort((a, b) => Vector3.Distance(a.transform.position, position).CompareTo(Vector3.Distance(b.transform.position, position)));
        return enemies;
    }

    public static Enemy ClosestEnemy(Vector3 position, float range)
    {
        var enemies = GetEnemiesInRangeSorted(position, range);
        return enemies.Count > 0 ? enemies[0] : null;
    }
}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> Enemies { get; private set; } = new();
    
    public float maxHp = 10;

    private float hp;

    public void Awake()
    {
        Enemies.Add(this);
    }
    
    void Start()
    {
        hp = maxHp;
    }

    public void OnDestroy()
    {
        Enemies.Remove(this);
    }

    public void Damage(float dmg) {
        hp -= dmg;
        
        if(hp < 1f) {
            Destroy(gameObject);
            return;
        }
        float scale = Mathf.Pow(hp / maxHp, 0.75f);
        transform.localScale = new Vector3(scale, scale, 0f);
    }
    
    public void Heal(float health) {
        hp = Mathf.Min(hp + health, maxHp);
        
        if(hp < 1f) return; // possible when an enemy is killed (with negative health) and healed in the same tick
        float scale = Mathf.Pow(hp / maxHp, 0.75f);
        transform.localScale = new Vector3(scale, scale, 0f);
    }
    
    public bool IsFullHp() {
        return hp >= maxHp;
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

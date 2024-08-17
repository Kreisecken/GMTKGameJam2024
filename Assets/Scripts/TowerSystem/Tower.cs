using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    public static List<Tower> Towers = new();

    public Vector3 MINIMUM_SCALE_THRESHOLD => new(0.1f, 0.1f, 0.1f);

    private Vector3 Scale => transform.localScale;

    [Header("Tower Components")]

    public Collider2D placementCollider;
    public SpriteRenderer spriteRenderer;

    [Header("Tower Placement & Growth Flags")]
    
    public bool placed = false;
    public bool colliding = false;

    [Header("Tower Properties")]
    public Vector3 growthRate = new(1f, 1f, 0f);
    public float cost = 1f;
    public float sellRate = 0.5f;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        placementCollider = GetComponent<Collider2D>();
        placementCollider.isTrigger = true;

        Towers.Add(this);
    }

    public void OnDestroy()
    {
        Towers.Remove(this);
    }

    public void Update()
    {
        if (placed) return;
        
        if (colliding) spriteRenderer.color = Color.red;
        else spriteRenderer.color = Color.gray;

    }

    public void Place()
    {
        placed = true;
        placementCollider.isTrigger = false;
        spriteRenderer.color = Color.white;
    }

    public void FixedUpdate()
    {
        List<Collider2D> colliders = new();
        colliding = placementCollider.Overlap(colliders) > 0; // maybe exclude player
    
        if (!placed) return;
    
        if (colliding) return;

        transform.localScale += Time.fixedDeltaTime * growthRate;

        if (transform.localScale.x < MINIMUM_SCALE_THRESHOLD.x || transform.localScale.y < MINIMUM_SCALE_THRESHOLD.y)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        Damage(new Vector2(damage, damage));
    }

    public void Damage(Vector2 damage)
    {
        transform.localScale -= new Vector3(damage.x, damage.y, 0);
    }

    public static List<Tower> GetTowersInRange(Vector3 position, float range)
    {
        List<Tower> towersInRange = new();

        foreach (var tower in Towers)
        {
            if (Vector3.Distance(position, tower.transform.position) <= range)
            {
                towersInRange.Add(tower);
            }
        }

        return towersInRange;
    }

    public static List<Tower> GetTowersInRangeSorted(Vector3 position, float range)
    {
        var towers = GetTowersInRange(position, range);
        towers.Sort((a, b) => Vector3.Distance(a.transform.position, position).CompareTo(Vector3.Distance(b.transform.position, position)));
        return towers;
    }

    public static Tower ClosestEnemy(Vector3 position, float range) 
    {
        var towers = GetTowersInRangeSorted(position, range);
        return towers.Count > 0 ? towers[0] : null;
    }
}

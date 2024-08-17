using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    public static List<Tower> Towers = new();

    public Vector3 MINIMUM_SCALE_THRESHOLD => new(0.1f, 0.1f, 0.1f);

    private Vector3 Scale => transform.localScale;

    [Header("Tower Components")]

    public BoxCollider2D placementCollider;
    public SpriteRenderer spriteRenderer;

    [Header("Tower Placement & Growth Flags")]
    
    public bool placed = false;
    public bool colliding = false;

    public bool left = false;
    public bool right = false;
    public bool up = false;
    public bool down = false;

    [Header("Tower Properties")]
    public Vector3 growthRate = new(1f, 1f, 0f);
    public float cost = 1f;
    public float sellRate = 0.5f;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        placementCollider = GetComponent<BoxCollider2D>();
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
    
        //if (colliding) return;

        //transform.localScale += Time.fixedDeltaTime * growthRate;

        CalculateCollisions();

        transform.localScale += new Vector3
        (
            left || right ? 0 : growthRate.x,
            up   || down  ? 0 : growthRate.y,
            0
        ) * Time.fixedDeltaTime;

        if (transform.localScale.x < MINIMUM_SCALE_THRESHOLD.x || transform.localScale.y < MINIMUM_SCALE_THRESHOLD.y)
        {
            Destroy(gameObject);
        }
    }

    public void CalculateCollisions()
    {
        left  = false;
        right = false;
        up    = false;
        down  = false;

        Vector3 position = transform.position;
        Vector3 scale    = transform.localScale;

        Vector3 topLeft     = position + new Vector3(-scale.x / 2,  scale.y / 2, 0);
        Vector3 topRight    = position + new Vector3( scale.x / 2,  scale.y / 2, 0);
        Vector3 bottomLeft  = position + new Vector3(-scale.x / 2, -scale.y / 2, 0);
        Vector3 bottomRight = position + new Vector3( scale.x / 2, -scale.y / 2, 0);

        Collider2D[] leftCollisions  = Physics2D.OverlapAreaAll(topLeft   + new Vector3(-0.1f,  0   ), bottomLeft );
        Collider2D[] rightCollisions = Physics2D.OverlapAreaAll(topRight  + new Vector3( 0.1f,  0   ), bottomRight);
        Collider2D[] upCollisions    = Physics2D.OverlapAreaAll(topLeft   + new Vector3( 0   ,  0.1f), topRight   );
        Collider2D[] downCollisions  = Physics2D.OverlapAreaAll(bottomLeft+ new Vector3( 0   , -0.1f), bottomRight);
       
        left  = leftCollisions .Length > 1;
        right = rightCollisions.Length > 1;
        up    = upCollisions   .Length > 1;
        down  = downCollisions .Length > 1;
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

    public static Tower ClosestTower(Vector3 position, float range) 
    {
        var towers = GetTowersInRangeSorted(position, range);
        return towers.Count > 0 ? towers[0] : null;
    }

    public void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        Vector3 scale    = transform.localScale;

        Vector3 topLeft     = position + new Vector3(-scale.x / 2,  scale.y / 2, 0);
        Vector3 topRight    = position + new Vector3( scale.x / 2,  scale.y / 2, 0);
        Vector3 bottomLeft  = position + new Vector3(-scale.x / 2, -scale.y / 2, 0);
        Vector3 bottomRight = position + new Vector3( scale.x / 2, -scale.y / 2, 0);

        var testA = topLeft   + new Vector3(-0.1f,  0   );//, bottomLeft );
        var testB = topRight  + new Vector3( 0.1f,  0   );//, bottomRight);
        var testC = topLeft   + new Vector3( 0   ,  0.1f);//, topRight   );
        var testD = bottomLeft+ new Vector3( 0   , -0.1f);//, bottomRight);

        Gizmos.color = Color.red;

        //Draw Area
        Gizmos.DrawLine(testA, bottomLeft );
        Gizmos.DrawLine(testB, bottomRight);
        Gizmos.DrawLine(testC, topRight   );
        Gizmos.DrawLine(testD, bottomRight);
    }
}

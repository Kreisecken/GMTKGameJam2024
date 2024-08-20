using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    public static List<Tower> Towers = new();

    public static Tower selectedTower = null;

    public static Vector3 MINIMUM_SCALE_THRESHOLD => new(0.1f, 0.1f, 0.1f);

    [Header("Tower Properties")]
    public Vector3 growthRate = new(0f, 0f, 0f);

    public Sprite icon;
    public Sprite banner;
    public string title = "Tower";
    public string description = "A tower";
    public int price = 100;

    //[Header("Tower Components")]
    public GameObject rangeIndicator;

    public BoxCollider2D  placementCollider;
    public SpriteRenderer spriteRenderer;

    //[Header("Tower Placement & Growth Flags")]
    public bool placed    = false;
    private bool colliding = false;

    private bool left  = false;
    private bool right = false;
    private bool up    = false;
    private bool down  = false;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        placementCollider = GetComponent<BoxCollider2D>();

        Towers.Add(this);
    }

    public void OnDestroy()
    {
        Towers.Remove(this);
    }

    public void FixedUpdate()
    {
        List<Collider2D> colliders = new();

        colliding = placementCollider.Overlap(colliders) > 0; // maybe exclude player

        if (!placed) return;

        if (selectedTower == this)
        {
            if (Keyboard.current[Key.Delete].wasPressedThisFrame)
            {
                Destroy(gameObject);
            }

            if (Keyboard.current[Key.Escape].wasPressedThisFrame)
            {
                selectedTower = null;
            }

            rangeIndicator.SetActive(true);
            //transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, new(0, 0f, 45f), 0.1f));
        }
        else
        {
            rangeIndicator.SetActive(false);
        }

        Debug.Log(selectedTower);

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

    public void Place()
    {
        placed = true;
        placementCollider.isTrigger = false;
    }

    public void OnMouseOver()
    {
        if (!placed) return;

        if (Keyboard.current[Key.Delete].isPressed)
        {
            Destroy(gameObject);
        }
    }

    public void OnMouseDown()
    {
        selectedTower = selectedTower == this ? null : this;
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

        List<Collider2D> leftCollisions = new();
        List<Collider2D> rightCollision = new();
        List<Collider2D> upCollisions   = new();
        List<Collider2D> downCollisions = new();

        int leftCollisionsCount  = Physics2D.OverlapArea(topLeft    + new Vector3(-0.1f,  0   ), bottomLeft , TowerInstantiator.Instance.growthBlockerFilter, leftCollisions);
        int rightCollisionsCount = Physics2D.OverlapArea(topRight   + new Vector3( 0.1f,  0   ), bottomRight, TowerInstantiator.Instance.growthBlockerFilter, rightCollision);
        int upCollisionsCount    = Physics2D.OverlapArea(topLeft    + new Vector3( 0   ,  0.1f), topRight   , TowerInstantiator.Instance.growthBlockerFilter, upCollisions  );
        int downCollisionsCount  = Physics2D.OverlapArea(bottomLeft + new Vector3( 0   , -0.1f), bottomRight, TowerInstantiator.Instance.growthBlockerFilter, downCollisions);

        left  = leftCollisionsCount  > 1;
        right = rightCollisionsCount > 1;
        up    = upCollisionsCount    > 1;
        down  = downCollisionsCount  > 1;
    }

    public void Damage(float damage)
    {
        Damage(new Vector2(damage, damage));
    }

    public void Damage(Vector2 damage)
    {
        transform.localScale -= (Vector3)damage;
    }

    public Rect GetRect()
    {
        return new Rect(transform.position - transform.localScale * 0.5f, transform.localScale);
    }

    public static List<Tower> GetTowersInRange(Vector3 position, float range)
    {
        List<Tower> towersInRange = new();

        foreach (var tower in Towers)
        {
            // if (Vector3.Distance(position, tower.transform.position) <= range)
            if (PointRectDistance(position, tower.GetRect()) <= range)
            {
                towersInRange.Add(tower);
            }
        }

        return towersInRange;
    }

    public static List<Tower> GetTowersInRangeSorted(Vector3 position, float range)
    {
        var towers = GetTowersInRange(position, range);
        // towers.Sort((a, b) => Vector3.Distance(a.transform.position, position).CompareTo(Vector3.Distance(b.transform.position, position)));
        towers.Sort((a, b) => PointRectDistance(position, a.GetRect()).CompareTo(PointRectDistance(position, b.GetRect())));
        return towers;
    }
    
    public static Tower ClosestTower(Vector3 position, float range) 
    {
        var towers = GetTowersInRangeSorted(position, range);
        return towers.Count > 0 ? towers[0] : null;
    }

    public static bool TryGetClosestTower(Vector3 position, float range, out Tower tower)
    {
        tower = ClosestTower(position, range);
        return tower != null;
    }
    
    private static float PointRectDistance(Vector2 point, Rect rect)
    {
        Vector2 distance = new Vector2(
            Mathf.Max(rect.xMin - point.x, 0f, point.x - rect.xMax),
            Mathf.Max(rect.yMin - point.y, 0f, point.y - rect.yMax)
        );
        return distance.magnitude;
    }
}

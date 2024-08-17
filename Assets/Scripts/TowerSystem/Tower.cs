using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TowerStats))]
public class Tower : MonoBehaviour
{
    public Vector3 MINIMUM_SCALE_THRESHOLD => new(0.1f, 0.1f, 0.1f);

    private float scale = 1f;

    public Collider2D placementCollider;

    public bool placed = false;
    public bool colliding = false;

    private TowerStats towerStats;
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        towerStats = GetComponent<TowerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        placementCollider = GetComponent<Collider2D>();
        placementCollider.isTrigger = true;
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

        transform.localScale += Time.fixedDeltaTime * towerStats.growthRate;

        if (transform.localScale.x < MINIMUM_SCALE_THRESHOLD.x || transform.localScale.y < MINIMUM_SCALE_THRESHOLD.y)
        {
            Destroy(gameObject);
        }
    }
}

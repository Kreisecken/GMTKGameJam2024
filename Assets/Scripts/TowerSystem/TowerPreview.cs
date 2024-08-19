using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPreview : MonoBehaviour
{
    private PlaceableTower placeableTower;

    public SpriteRenderer spriteRenderer = null;
    private BoxCollider2D placementCollider = null;

    private Action<Tower> successTowerPlacedCallback = null;
    private Action<Tower> failTowerPlacedCallback = null;

    public float time = 0.5f;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (placeableTower == null) return;

        time -= Time.deltaTime;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            StopPreview();
            failTowerPlacedCallback?.Invoke(placeableTower);
            return;
        }

        if (placementCollider.Overlap(TowerInstantiator.Instance.growthBlockerFilter, new Collider2D[5]) < 1)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.red;
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TowerInstantiator.InstantiateTower(placeableTower, transform.position, (tower) =>
            {
                successTowerPlacedCallback?.Invoke(tower);
            }, (tower) =>
            {
                failTowerPlacedCallback?.Invoke(tower);
            });

            StopPreview();
        }
    }

    public void PreviewTower(PlaceableTower tower, Action<Tower> successTowerPlacedCallback = null, Action<Tower> failTowerPlacedCallback = null)
    {
        placeableTower = tower;

        this.successTowerPlacedCallback = successTowerPlacedCallback;
        this.failTowerPlacedCallback = failTowerPlacedCallback;

        spriteRenderer.sprite = tower.icon;

        BoxCollider2D colliderCopy = tower.GetComponent<BoxCollider2D>();
        placementCollider = gameObject.AddComponent<BoxCollider2D>();
        placementCollider.isTrigger = true;
        placementCollider.size = colliderCopy.size;

        gameObject.SetActive(true);
    }

    public void StopPreview()
    {
        Destroy(gameObject);
    }
}

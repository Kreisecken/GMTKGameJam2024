using System;
using UnityEngine;
using UnityEngine.InputSystem;

#nullable enable
public class TowerInstantiator : MonoBehaviour
{
    public Tower towerPrefab; // debug

    private Tower? tower = null;

    private static TowerInstantiator towerInstantiator;

    public void Start()
    {
        if (towerInstantiator == null)
        {
            towerInstantiator = this;
            return;
        }

        Destroy(towerInstantiator.gameObject);
        Debug.Log("TowerInstantiator already exists. Destroying new instance.");
    }

    public void Update()
    {
        if (tower == null) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        tower.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && !tower.colliding)
        {
            tower.Place();
            tower = null;
        }
    }

    public void OnMouseDown() // DEBUG
    {
        if (tower != null) return;

        InstantiateTower(towerPrefab);
    }

    public static void InstantiateTower(Tower towerPrefab)
    {
        towerInstantiator.tower = Instantiate(towerPrefab.gameObject).GetComponent<Tower>();
        towerInstantiator.tower.gameObject.SetActive(true);
    }
}

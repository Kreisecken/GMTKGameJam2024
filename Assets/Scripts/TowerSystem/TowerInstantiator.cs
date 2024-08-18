using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerInstantiator : MonoBehaviour
{
    public static TowerInstantiator Instance;

    [Header("TowerSystem Properties")]
    public Tower towerPrefab; // debug

    public ContactFilter2D growthBlockerFilter;

    private Tower towerThatsGettingPlaced = null;
    private Action<Tower> successTowerPlacedCallback = null;

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(Instance.gameObject);
        Debug.Log("TowerInstantiator already exists. Destroying new instance.");
    }

    public void Update()
    {
        if (towerThatsGettingPlaced == null) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        towerThatsGettingPlaced.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && !towerThatsGettingPlaced.colliding)
        {
            towerThatsGettingPlaced.Place();
            towerThatsGettingPlaced = null;
            successTowerPlacedCallback?.Invoke(towerThatsGettingPlaced);
        }
    }

    public void OnMouseDown() // DEBUG
    {
        if (towerThatsGettingPlaced != null) return;

        InstantiateTower(towerPrefab);
    }

    public static void InstantiateTower(Tower towerPrefab, Action<Tower> callback = null)
    {
        if (Instance.towerThatsGettingPlaced != null) 
        {
            Destroy(Instance.towerThatsGettingPlaced.gameObject);
        }

        Instance.towerThatsGettingPlaced = Instantiate(towerPrefab.gameObject).GetComponent<Tower>();
        Instance.towerThatsGettingPlaced.gameObject.SetActive(true);

        Instance.successTowerPlacedCallback = callback;
    }
}

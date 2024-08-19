using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerInstantiator : MonoBehaviour
{
    public static TowerInstantiator Instance;

    [Header("TowerSystem Properties")]
    public TowerPreview towerPreviewPrefab;
    public bool IsPreviewing { get; private set; }

    public ContactFilter2D growthBlockerFilter;

    private TowerPreview towerPreview;

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

    public static void PlaceTowerUsingPreview(PlaceableTower towerPrefab, Vector3 position, Action<Tower> successTowerPlacedCallback = null, Action<Tower> failTowerPlacedCallback = null)
    {
        Instance.IsPreviewing = true;

        GameObject towerPreview = Instantiate(Instance.towerPreviewPrefab.gameObject);
        towerPreview.transform.position = position;
        Instance.towerPreview = towerPreview.GetComponent<TowerPreview>();

        towerPreview.SetActive(true);

        Instance.towerPreview.PreviewTower(towerPrefab, (tower) =>
        {
            Instance.IsPreviewing = false;
            successTowerPlacedCallback?.Invoke(tower);
        }, (tower) =>
        {
            Instance.IsPreviewing = false;
            failTowerPlacedCallback?.Invoke(tower);
        });
    }

    public static void CancelPreview()
    {
        if (Instance.IsPreviewing)
        {
            Instance.towerPreview.StopPreview();
            Instance.IsPreviewing = false;
        }
    }

    public static void InstantiateTower(PlaceableTower towerPrefab, Vector3 position, Action<Tower> successTowerPlacedCallback = null, Action<Tower> failTowerPlacedCallback = null)
    {
        GameObject tower = Instantiate(towerPrefab.gameObject);
        tower.transform.position = position;
        tower.SetActive(true);
        PlaceableTower towerComponent = tower.GetComponent<PlaceableTower>();
        towerComponent.Place();
        successTowerPlacedCallback?.Invoke(towerComponent);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class TowerInstantiator : MonoBehaviour
{
    public GameObject towerPrefab; // debug

    private GameObject towerPreview;
    private Tower tower;

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
        if (towerPreview == null) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        towerPreview.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && !tower.colliding)
        {
            tower.Place();
            towerPreview = null;
        }
    }

    public void OnMouseDown()
    {
        if (towerPreview != null) return;

        InstantiateTower(towerPrefab);
    }

    public static void InstantiateTower(GameObject towerPrefab)
    {
        towerInstantiator.towerPreview = Instantiate(towerPrefab);
        towerInstantiator.tower = towerInstantiator.towerPreview.GetComponent<Tower>();
        towerInstantiator.towerPreview.SetActive(true);
    }
}

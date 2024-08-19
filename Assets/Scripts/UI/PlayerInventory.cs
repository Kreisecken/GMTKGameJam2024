using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public TowerItem[] buttons = new TowerItem[3];
    public PlaceableTower[] towers  = new PlaceableTower[3];

    public int selected = 1;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(Instance.gameObject);
        Debug.Log("PlayerInventory already exists. Destroying new instance.");
    }

    public void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = towers[i] == null ? null : towers[i].GetComponent<SpriteRenderer>().sprite;
            buttons[i].tower = towers[i];
        }

        if      (Keyboard.current[Key.Digit1].wasPressedThisFrame) {selected = 0; TowerInstantiator.CancelPreview();}
        else if (Keyboard.current[Key.Digit2].wasPressedThisFrame) {selected = 1; TowerInstantiator.CancelPreview();}
        else if (Keyboard.current[Key.Digit3].wasPressedThisFrame) {selected = 2; TowerInstantiator.CancelPreview();}
        
        if (selected != -1 && (Keyboard.current[Key.Escape].isPressed || towers[selected] == null)) selected = -1;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = i == selected ? Color.green : Color.white;
        }

        if (selected == -1) return;

        if (TowerInstantiator.Instance.IsPreviewing) return;

        TowerInstantiator.PlaceTowerUsingPreview(towers[selected], Mouse.current.position.ReadValue(), (tower) =>
        {
            towers[selected] = null;
            selected = -1;
        }, (tower) =>
        {
            //towers[selected] = tower;
        });
    }

    public bool TryAddTower(PlaceableTower tower)
    {
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] != null) continue;

            towers[i] = tower;
            return true;
        }

        return false;
    }

    public bool TryRemoveTower(TowerItem towerItem)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != towerItem) continue;

            towers[i] = null;
            return true;
        }

        return false;
    }
}
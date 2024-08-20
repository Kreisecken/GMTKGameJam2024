using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public TowerItem[] buttons = new TowerItem[3];
    public PlaceableTower[] towers  = new PlaceableTower[3];

    public int selected = 1;

    public TMP_Text moneyLabel;
    public TMP_Text greenCrystalLabel;
    public TMP_Text blueCrystalLabel;

    private int money = 1000;
    private int greenCrystal = 1000;
    private int blueCrystal = 1000;

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
    
    void Start()
    {
        moneyLabel.text = money.ToString();
        greenCrystalLabel.text = greenCrystal.ToString();
        blueCrystalLabel.text = blueCrystal.ToString();
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
            // ShopMenu.Instance.currentTowerItem = null;
            ShopMenu.Instance.ClearCurrentTowerItem();
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
    
    public int GetMoney()
    {
        return money;
    }

    public int GetGreenCrystal()
    {
        return greenCrystal;
    }

    public int GetBlueCrystal()
    {
        return blueCrystal;
    }
    
    public void AddMoney(int amount)
    {
        money += amount;
        moneyLabel.text = money.ToString();
    }
    
    public void RemoveMoney(int amount)
    {
        money -= amount;
        moneyLabel.text = money.ToString();
    }

    public void AddGreenCrystal(int amount)
    {
        greenCrystal += amount;
        greenCrystalLabel.text = greenCrystal.ToString();
    }

    public void AddBlueCrystal(int amount)
    {
        blueCrystal += amount;
        blueCrystalLabel.text = blueCrystal.ToString();
    }

    public void RemoveGreenCrystal(int amount)
    {
        greenCrystal -= amount;
        greenCrystalLabel.text = greenCrystal.ToString();
    }

    public void RemoveBlueCrystal(int amount)
    {
        blueCrystal -= amount;
        blueCrystalLabel.text = blueCrystal.ToString();
    }
}

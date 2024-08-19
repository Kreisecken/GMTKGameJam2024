using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public static ShopMenu Instance;

    public GameObject shopMenuContent;

    public List<PlaceableTower> towers = new();
    public ShopItem shopItemPrefab;

    public ShopItem currentShopItem;
    public TowerItem currentTowerItem;

    public Transform shopItemContainer;

    public bool isOpen;

    public GameObject shopDescriptGameObject;

    public Image banner;
    public TMP_Text title;
    public TMP_Text description;

    public Button button;
    public bool isBuying;

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            Close();
    
            foreach (PlaceableTower tower in towers)
            {
                GameObject shopItemGameObject = Instantiate(shopItemPrefab.gameObject, shopItemContainer);
                ShopItem shopItem = shopItemGameObject.GetComponent<ShopItem>();
                shopItem.SetTower(tower);
                shopItemGameObject.SetActive(true);
            }

            button.onClick.AddListener(() =>
            {
                if (isBuying) Buy();
                else Sell();
            });
            
            return;
        }

        Destroy(Instance.gameObject);
        Debug.Log("ShopMenu already exists. Destroying new instance.");
    }

    void Update()
    {
        if(Keyboard.current.tabKey.wasPressedThisFrame) 
        {
            if (isOpen) Close();
            else Open();
        }

        if (!isOpen) return;

        //if (currentShopItem == ShopScrollViewSnap.Instance.selectedShopItem) return;
//
        //currentShopItem = ShopScrollViewSnap.Instance.selectedShopItem;
//
        //UpdateMenu(currentShopItem, true);
    }

    public void CloseMenu()
    {
        shopDescriptGameObject.SetActive(false);
        banner.sprite = null;
        title.text = "";
        description.text = "";
    }

    public void UpdateMenu(ShopItem shopItem)
    {
        currentShopItem = shopItem;
        isBuying = true;
        UpdateMenu(shopItem.tower);
    }

    public void UpdateMenu(TowerItem towerItem)
    {
        shopDescriptGameObject.SetActive(true);
        currentTowerItem = towerItem;
        isBuying = false;
        UpdateMenu(towerItem.tower);
    }


    public void UpdateMenu(PlaceableTower tower)
    {
        if (tower == null)
        {
            CloseMenu();
            return;
        }

        shopDescriptGameObject.SetActive(true);

        banner.sprite = tower.spriteRenderer.sprite;
        title.text = tower.name;
        description.text = tower.description;

        button.GetComponentInChildren<TMP_Text>().text = isBuying ? "Buy" : "Sell";
    }

    public void Open()
    {
        isOpen = true;
        shopMenuContent.SetActive(true);
    }

    public void Close()
    {
        isOpen = false;
        shopMenuContent.SetActive(false);
    }

    public void Buy()
    {
        if (currentShopItem == null) return;

        if (!PlayerInventory.Instance.TryAddTower(currentShopItem.tower)) return;

        Debug.Log("Bought " + currentShopItem.tower.name);
    }

    public void Sell()
    {
        if (currentTowerItem == null) return;

        if (!PlayerInventory.Instance.TryRemoveTower(currentTowerItem)) return;

        Debug.Log("Sold " + currentShopItem.tower.name);

        CloseMenu();
    }
}

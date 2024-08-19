using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image image;
    public TMP_Text price;

    public PlaceableTower tower;

    public void SetTower(PlaceableTower tower)
    {
        this.tower = tower;

        image      .sprite = tower.spriteRenderer.sprite;
        //price      .text   = tower.price.ToString();
    }
}

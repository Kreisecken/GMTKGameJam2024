using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TowerItem : MonoBehaviour, IPointerClickHandler
{
    public PlaceableTower tower;
    public int index;

    public RectTransform rectTransform;
    public Image image;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MouseEnter");
        if (tower == null) return;
        ShopMenu.Instance.UpdateMenu(this);
    }
}

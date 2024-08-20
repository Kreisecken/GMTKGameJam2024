using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerScaler : MonoBehaviour
{
    public Button buttonHorizontal;
    public Slider HorizontalSlider;
    public TMP_Text buttonHorizontalText;
    public Button buttonVertical;
    public TMP_Text buttonVerticalText;
    public Slider VerticalSlider;

    public TMP_Text no;


    public void Start()
    {
        buttonHorizontal.gameObject.SetActive(false);
        buttonVertical.gameObject.SetActive(false);
        no.gameObject.SetActive(true);

        buttonHorizontal.onClick.AddListener(BuyHorizontal);
        buttonVertical.onClick.AddListener(BuyVertical);
    }

    public void Update()
    {
        if (Tower.selectedTower == null)
        {
            buttonHorizontal.gameObject.SetActive(false);
            buttonVertical.gameObject.SetActive(false);
            no.gameObject.SetActive(true);
            return;
        }

        buttonHorizontal.gameObject.SetActive(true);
        buttonVertical.gameObject.SetActive(true);
        no.gameObject.SetActive(false);

        HorizontalSlider.maxValue = Tower.selectedTower.horizontalMax;
        HorizontalSlider.value = Tower.selectedTower.growthBuyed.x;

        VerticalSlider.maxValue = Tower.selectedTower.verticalMax;
        VerticalSlider.value = Tower.selectedTower.growthBuyed.y;

        buttonHorizontalText.text = Tower.selectedTower.horizontalPrice.ToString();
        buttonVerticalText.text = Tower.selectedTower.verticalPrice.ToString();
    }

    public void BuyVertical()
    {
        if (Tower.selectedTower == null) return;
        Tower.selectedTower.BuyVertical();
    }

    public void BuyHorizontal()
    {
        if (Tower.selectedTower == null) return;
        Tower.selectedTower.BuyHorizontal();
    }
}

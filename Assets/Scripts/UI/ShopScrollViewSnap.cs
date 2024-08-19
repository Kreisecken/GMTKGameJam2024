using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopScrollViewSnap : MonoBehaviour
{
    public static ShopScrollViewSnap Instance;

    public GameObject scrollBarGameObject;
    public Transform content;

    private Scrollbar scrollBar;
    private float scrollPosition = 0;
    private float[] positions;
    private float distance;

    public ShopItem selectedShopItem;

    public void Awake()
    {
        scrollBar = scrollBarGameObject.GetComponent<Scrollbar>();

        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(Instance.gameObject);
        Debug.Log("ShopScrollViewSnap already exists. Destroying new instance.");
    }

    void Update()
    {
        positions = new float[content.transform.childCount];
        distance = 1f / (positions.Length - 1f);

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = distance * i;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            if (scrollPosition >= positions[i] + (distance / 2) || scrollPosition <= positions[i] - (distance / 2)) continue;

            Transform child = content.transform.GetChild(i);

            if (child.TryGetComponent(out ShopItem shopItem))
            {
                if (selectedShopItem == shopItem) continue;
                selectedShopItem = shopItem;
                ShopMenu.Instance.UpdateMenu(shopItem);
            }
        }

        if (Mouse.current.leftButton.isPressed)
        {
            scrollPosition = scrollBar.value;
        }
        else
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (scrollPosition >= positions[i] + (distance / 2) ||
                    scrollPosition <= positions[i] - (distance / 2))
                    continue;

                scrollBar.value = Mathf.Lerp(scrollBar.value, positions[i], 0.01f);
            }
        }

        for (int i = 0; i < positions.Length; i++)
        {
            if (scrollPosition >= positions[i] + (distance / 2) || scrollPosition <= positions[i] - (distance / 2)) continue;

            Transform childA = content.transform.GetChild(i);

            childA.localScale = Vector2.Lerp(childA.localScale, Vector2.one, 0.01f);

            for (int a = 0; a < positions.Length; a++)
            {
                if (a == i) continue;

                Transform childB = content.transform.GetChild(a);

                childB.localScale = Vector2.Lerp(childB.localScale, Vector2.one * (0.8f), 0.01f);
            }
        }
    }
}

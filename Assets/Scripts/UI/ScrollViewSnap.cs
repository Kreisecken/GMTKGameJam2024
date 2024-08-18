using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScrollViewSnap : MonoBehaviour
{
    public GameObject scrollBarGameObject;
    public Scrollbar scrollBar;
    public float scrollPosition  = 0;
    public float[] positions;
    public float distance;

    public void Start()
    {
        scrollBar = scrollBarGameObject.GetComponent<Scrollbar>();
    }

    void Update()
    {
        positions = new float[transform.childCount];
        distance  = 1f / (positions.Length - 1f);

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = distance * i;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            scrollPosition = scrollBar.value;
        } else 
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (scrollPosition >= positions[i] + (distance / 2) ||
                    scrollPosition <= positions[i] - (distance / 2)) 
                    continue;

                scrollBar.value = Mathf.Lerp(scrollBar.value, positions[i], 0.1f);
            }
        }
        
        for (int i = 0; i < positions.Length; i++)
        {
            if (scrollPosition >= positions[i] + (distance / 2) || scrollPosition <= positions[i] - (distance / 2)) continue;

            Transform childA = transform.GetChild(i);

            childA.localScale = Vector2.Lerp(childA.localScale, Vector2.one, 0.1f);

            for (int a = 0; a < positions.Length; a++)
            {
                if (a == i) continue;

                Transform childB = transform.GetChild(a);

                childB.localScale = Vector2.Lerp(childB.localScale, Vector2.one * (0.8f - Mathf.Abs(positions[i] - positions[a]) * 1.5f), 0.1f);
            }
        }
    }
}

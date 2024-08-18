using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public TowerItem[] buttons = new TowerItem[3];
    public Tower [] towers  = new Tower[3];

    public int selected = 1;

    public void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = towers[i] == null ? null : towers[i].GetComponent<SpriteRenderer>().sprite;
        }

        if      (Keyboard.current[Key.Digit1].isPressed) selected = 0;
        else if (Keyboard.current[Key.Digit2].isPressed) selected = 1;
        else if (Keyboard.current[Key.Digit3].isPressed) selected = 2;
        
        if (selected != -1 && (Keyboard.current[Key.Escape].isPressed || towers[selected] == null)) selected = -1;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = i == selected ? Color.green : Color.white;
        }

        if (selected == -1) return;

        TowerInstantiator.InstantiateTower(towers[selected], (tower) =>
        {
            towers[selected] = null;
            selected = -1;
        });
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class MainTower : Tower
{
    public new void Awake()
    {
        base.Awake();
        Place();
    }
}
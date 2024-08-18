using UnityEngine;
using UnityEngine.InputSystem;

public class Objective : MonoBehaviour
{
    public float health = 10;

    public void Update()
    {
        transform.localScale = health * Vector3.one;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class TowerStats : MonoBehaviour
{
    public Vector3 growthRate = new Vector3(0.1f, 0.1f, 0f);
    
    public float range = 1f;

    public float attackRate = 1f;

    public float damage = 1f;

    public float cost = 1f;

    public float sellRate = 0.5f;
}
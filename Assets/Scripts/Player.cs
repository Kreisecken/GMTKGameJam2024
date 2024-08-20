using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
        Debug.Log("Player already exists. Destroying new instance.");
    }

    public void Damage(float damage)
    {

    }
}

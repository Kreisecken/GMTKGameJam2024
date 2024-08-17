using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3f;
    
    private InputAction moveAction;
    
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>().normalized;
        GetComponent<Rigidbody2D>().linearVelocity = movement * movementSpeed;
    }
}

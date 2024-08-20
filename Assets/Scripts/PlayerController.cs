using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3f;
    
    private InputAction moveAction;
    
    private Vector2 moveInput = Vector2.zero;
    private Rigidbody2D rb;
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if(moveInput.x < 0f) animator.SetBool("FacingRight", false);
        if(moveInput.x > 0f) animator.SetBool("FacingRight", true);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * movementSpeed;
    }
}

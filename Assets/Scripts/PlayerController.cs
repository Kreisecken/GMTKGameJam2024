using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3f;
    
    private InputAction moveAction;
    private InputAction attackAction;
    
    private bool attackInput = false;
    private bool attacked = false;
    private Vector2 moveInput = Vector2.zero;
    private Rigidbody2D rb;
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if(moveInput.x < 0f) animator.SetBool("FacingRight", false);
        if(moveInput.x > 0f) animator.SetBool("FacingRight", true);
        
        attackInput = attackAction.IsPressed();
        if(!attackInput) attacked = false;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * movementSpeed;
        
        if(attackInput && !attacked) {
            attacked = true;
            // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f);
            List<Enemy> enemies = Enemy.GetEnemiesInRange(transform.position, 3f);
            foreach(Enemy enemy in enemies) {
                enemy.Damage(5f);
            }
        }
    }
}

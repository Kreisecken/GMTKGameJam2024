using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public GameObject target;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Vector2 move = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = move * movementSpeed;
    }
}

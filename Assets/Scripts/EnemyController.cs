using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public GameObject target;
    
    void Start()
    {
    }

    void Update()
    {
        Vector2 move = (target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = move * movementSpeed;
    }
}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public int hp = 10;
    public GameObject target;
    
    void Start()
    {
    }

    void Update()
    {
        Vector2 move = (target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = move * movementSpeed;
    }
    
    public void Damage(int dmg) {
        hp -= dmg;
        if(hp <= 0) Destroy(gameObject);
    }
}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float maxHp = 10;
    public GameObject target;
    
    private float hp;
    
    void Start()
    {
        hp = maxHp;
    }

    void FixedUpdate()
    {
        Vector2 move = (target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = move * movementSpeed;
    }
    
    public void Damage(float dmg) {
        hp -= dmg;
        
        float scale = Mathf.Pow(hp / maxHp, 0.75f);
        transform.localScale = new Vector3(scale, scale, 0f);
        if(hp < 1f) Destroy(gameObject);
    }
    
    public void Heal(float health) {
        hp = Mathf.Min(hp + health, maxHp);
        
        float scale = Mathf.Pow(hp / maxHp, 0.75f);
        transform.localScale = new Vector3(scale, scale, 0f);
    }
    
    public bool IsFullHp() {
        return hp >= maxHp;
    }
}

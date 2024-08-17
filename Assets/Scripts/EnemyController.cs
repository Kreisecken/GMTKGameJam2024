using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public int maxHp = 10;
    public GameObject target;
    public GameObject hpBarFull;
    
    private int hp;
    
    void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        Vector2 move = (target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = move * movementSpeed;
    }
    
    public void Damage(int dmg) {
        hp -= dmg;
        float hpBarValue = (float) hp / maxHp;
        hpBarFull.transform.localScale = new Vector3(hpBarValue, 1f, 1f);
        hpBarFull.transform.localPosition = new Vector3(0.5f * hpBarValue - 0.5f, 0f, -1f);
        if(hp <= 0) Destroy(gameObject);
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy enemy;
    private float damage;
    [SerializeField] private float splashRadius = 0f;
    private float radiusOfTower;
    private Transform tower;
    private Rigidbody2D rb;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetRadiusOfTower(float radius)
    {
        radiusOfTower = radius;
    }

    public void SetTower(Transform _tower)
    {
        tower = _tower;
    }

    public float GetSplashRadius()
    {
        return splashRadius;
    }

    private void Update()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(tower.position.x, tower.position.y));
        if (distance >= radiusOfTower + 0.2)
        {
            Pools.ReturnToPool(gameObject.name, gameObject);
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //Vynulovanie rýchlosti guľky
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            //Destroy(gameObject);
            Pools.ReturnToPool(gameObject.name, gameObject);

            enemy = collision.collider.GetComponent<Enemy>();
            enemy.GetHit(damage);
        }
    }

    private void OnDisable()//metóda, ktorá vykoná pri ničení strely
    {
        if (splashRadius > 0f)
        {
            DoSplashDamage();
        }
    }

    private void DoSplashDamage()
    {
        //vytvoríme kruh so stredom v strede strely a polomerom, ktorý vieme nastaviť
        //tento kruh je vlastne collider, ktorý zistí všetky kolízie vo svojom dosahu
        Collider2D[] collidersInExpolsion = Physics2D.OverlapCircleAll(transform.position, splashRadius);

        foreach (Collider2D collider2D in collidersInExpolsion)
        {
            if (collider2D.CompareTag("enemy")) //ak sme trafili nepriatela
            {
                //výpočet poškodenia v závisloti od vzdialenosti od strely
                Vector2 closestPoint = collider2D.ClosestPoint(transform.position);
                float distance = Vector3.Distance(closestPoint, transform.position);
                float damagePercentage = Mathf.InverseLerp(splashRadius, 0, distance);
                
                enemy = collider2D.GetComponent<Enemy>();
                enemy.GetHit(damagePercentage * damage);
            }
        }
    }
}

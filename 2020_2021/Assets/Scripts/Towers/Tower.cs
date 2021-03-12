using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    private List<Transform> enemyList = new List<Transform>();
    protected Transform target;
    private Vector2 direction;

    protected Quaternion startRotation;
    //private int lastLength;

    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] private Transform forwardPoint;
    [SerializeField] protected float force = 10f;
    [SerializeField] protected float reloadTime = 1f;
    private float splashRadius;
    protected bool canShoot;
    [SerializeField] protected float turnSpeed = 10f;
    protected float timer;

    [SerializeField] protected string sfxName;

    protected Rigidbody2D newRB2D;

    protected GameObject newBullet;

    protected CircleCollider2D _collider;  //na zmenenie dosahu veze 
    protected float startRadius;
    protected float radius;

    [SerializeField] private GameObject rangeObject;

    protected Dictionary<string, string> stats = new Dictionary<string, string>();

    private bool inShoot = false;

    public float GetRange()
    {
        return _collider.radius;
    }

    public virtual void AddToEnemyList(Transform newTransform)
    {
        enemyList.Add(newTransform);
        FindNewTarget();
    }

    public virtual void RemoveFromEnemyList(Transform newTransform)
    {
        enemyList.Remove(newTransform);
        FindNewTarget();
    }

    protected virtual void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        startRadius = _collider.radius;
        startRotation = firePoint.transform.rotation;

        stats.Add("Level", level.ToString());
        stats.Add("Range", startRadius.ToString());

        stats.Add("Damage", level.ToString());
        stats.Add("Reload", reloadTime.ToString());

        splashRadius = bulletPrefab.GetComponent<Bullet>().GetSplashRadius();

        if (splashRadius > 0)
        {
            stats.Add("Splash Radius", splashRadius.ToString("F2"));
        }
    }

    private void OnEnable()
    {
        level = 1;
        //lastLength = 0;
        timer = reloadTime;
        _collider.radius = startRadius;
        rangeObject.SetActive(false);
        firePoint.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    protected virtual void FindNewTarget()
    {
        if (enemyList.Count > 0)
        {
            if (target != enemyList[0])
            {
                target = enemyList[0];
            }
        }
        else
        {
            target = null;
        }
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            Aim();
            if (timer <= 0f && canShoot)
            {
                Shoot();
                timer = reloadTime;
            }
        }
        else if (!inShoot)
        {
            Quaternion newRotation = Quaternion.Lerp(firePoint.transform.rotation, startRotation, Time.deltaTime * turnSpeed);
            firePoint.transform.rotation = newRotation;
        }
        UpdateOtherThings();
    }

    protected void UpdateOtherThings()
    {
        if (GameManager.GetSelectedObject() == gameObject)
        {
            if (level <= 5)
            {
                radius = startRadius + 0.1f * level;
            }

            _collider.radius = radius;
            rangeObject.transform.localScale = new Vector3(0.34f * radius, 0.34f * radius, 0);
            if (!rangeObject.activeSelf)
            {
                rangeObject.SetActive(true);
            }

        }
        else
        {
            if (rangeObject.activeSelf)
            {
                rangeObject.SetActive(false);
            }
        }

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    protected void Aim()
    {
        if (!inShoot)
        {
            direction = firePoint.position - target.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //aby nestrielal, kym nie je úplne dotoceny
            Vector2 currentDirection = new Vector2(firePoint.position.x - forwardPoint.position.x, firePoint.position.y - forwardPoint.position.y);

            //uhol dvoch vektorov
            float differenceAngle = Mathf.Acos((direction.x * currentDirection.x + direction.y * currentDirection.y) / (direction.magnitude * currentDirection.magnitude));
            differenceAngle *= Mathf.Rad2Deg; //na stupne z radianov

            if (differenceAngle > 10)
            {
                canShoot = false;
            }
            else
            {
                canShoot = true;
            }

            Quaternion newQuat = Quaternion.Euler(Vector3.forward * (angle - 90f));

            //Plynulé otáčanie
            Quaternion newRotation = Quaternion.Lerp(firePoint.transform.rotation, newQuat, Time.deltaTime * turnSpeed);
            firePoint.rotation = newRotation;
        }
    }

    protected void SpawnBullet(GameObject bullet, Vector3 position, Quaternion rotation)
    {
        newBullet = Pools.GetFromPool(bullet.name, bullet);
        newBullet.name = bullet.name;
        newBullet.transform.position = position;
        newBullet.transform.rotation = rotation;
        newBullet.SetActive(true);
    }

    protected virtual float CalculateDMG()
    {
        if (splashRadius > 0) //aby mal mortar mierne vyšší dmg
        {
            return 30 + (level * 50) / 2;
        }
        else
        {
            return 20 + (level * 50) / 2;
        }
    }

    protected virtual void Shoot()
    {
        inShoot = true;
        AudioManager.Instance.Play(sfxName, true);

        SpawnBullet(bulletPrefab, firePoint.position, firePoint.rotation);
        newRB2D = newBullet.GetComponent<Rigidbody2D>();//komponent na prácu s fyzikou
        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.SetDamage(CalculateDMG());
        bullet.SetTower(transform);
        bullet.SetRadiusOfTower(_collider.radius);

        newRB2D.AddForce(firePoint.up * -1f * force, ForceMode2D.Impulse);
        inShoot = false;
    }

    public virtual Dictionary<string, string> GetStats()
    {
        stats["Level"] = level.ToString();
        stats["Range"] = radius.ToString();

        stats["Damage"] = CalculateDMG().ToString();

        return stats;
    }
}

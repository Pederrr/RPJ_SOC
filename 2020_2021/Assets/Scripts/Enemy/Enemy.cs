using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 300f;
    protected float startMaxhealth;
    protected float health;
    [SerializeField] private float startSpeed = 10f;
    private float speed;
    [SerializeField] private int damage = 1;

    [SerializeField] protected HealthBar healthBar;

    private List<Tower> currentTowers = new List<Tower>();

    [SerializeField] protected int reward = 10;

    [SerializeField] protected GameObject particle;

    protected bool first = true;

    protected int level = 1;

    protected Light2D enemyLight;
    protected float startLightInstensity;

    public float GetHealth()
    {
        return health;
    }

    public int GetDamage()
    {
        return damage * level;
    }

    public int GetReward()
    {
        int newReward = reward * level;
        return newReward;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void ResetSpeed()
    {
        speed = startSpeed;
    }

    public void GetHit(float damage)
    {
        health -= damage;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int _level)
    {
        level = _level;
    }

    public void Slow(float percentage)
    {
        if (speed != startSpeed)
        {
            //Ak naraz viac laserov, chcem aby sa aplikovalo len najvacsie spomalenie
            //Predtým sa mi niekedy zobralo spomalenie lasera, kotrý sa nepriatela dotkol ako posledný
            if (speed > startSpeed*percentage) 
            {
                speed = startSpeed * percentage;
            }
        }
        else
        {
            speed = startSpeed * percentage;
        }

        if (speed < 0.02f)
        {
            speed = 0.02f;
        }
    }

    private void Awake()
    {
        startMaxhealth = maxHealth;
        enemyLight = transform.GetChild(1).GetComponent<Light2D>();
        startLightInstensity = enemyLight.intensity;
    }

    private void OnEnable()
    {
        speed = startSpeed;
        maxHealth = startMaxhealth + 2 * (level - 1);
        health = maxHealth;
        enemyLight.intensity = startLightInstensity;
    }

    //funkcia sa spustí pri kontakte s triggerom
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("tower"))//zistím, či je to veža
        {
            Tower script = collider.GetComponent<Tower>(); 
            currentTowers.Add(script);
            script.AddToEnemyList(transform);
        }

        if (collider.CompareTag("end"))
        {
            //Destroy(gameObject);
            Pools.ReturnToPool(gameObject.name, gameObject);

            MainTower.GetHit(damage * level); //aby sa damage scaloval s levlom    
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("tower"))
        {
            Tower script = collider.GetComponent<Tower>();
            currentTowers.Remove(script);
            script.RemoveFromEnemyList(transform);
        }
    }

    private void OnDisable()
    {
        if (!first)
        {
            Spawner.DecrementActiveEnemies();

            if (currentTowers != null)
            {
                for (int i = 0; i < currentTowers.Count; i++)
                {
                    currentTowers[i].RemoveFromEnemyList(transform);
                }
            }
        }
        else
        {
            first = false;
        }

        if (GameManager.GetSelectedObject() == gameObject) //ak som ho mal selectnuty a zomrie, tak sa deselectne
        {
            GameManager.SetSelectedObject(null);
        }
    }

    protected virtual void Update()
    {
        healthBar.SetScale(health / maxHealth);      

        if (health <= 0)
        { 
            SpawnEffect(particle, transform.position, particle.transform.rotation);
            Pools.ReturnToPool(gameObject.name, gameObject);
            MainTower.AddEnergy(GetReward());
        }

        if (GameManager.GetSelectedObject() == gameObject)
        {
            enemyLight.intensity = 2;
        }
        else
        {
            enemyLight.intensity = startLightInstensity;
        }
    }

    protected void SpawnEffect(GameObject effectObject, Vector3 position, Quaternion rotation)
    {
        GameObject particles = Pools.GetFromPool(effectObject.name, effectObject);
        particles.name = effectObject.name;
        particles.transform.position = position;
        particles.transform.rotation = rotation;
        particles.SetActive(true);
        particles.GetComponent<ParticleSystem>().Play();
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.SetSelectedObject(gameObject);
            GameManager.SetActiveUI(null);
        }
    }
}

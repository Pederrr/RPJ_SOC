using UnityEngine;

public class EnemyTriangle : Enemy
{
    [SerializeField] private GameObject child;
    [SerializeField] private Transform[] spawnPoints;

    protected override void Update()
    {
        healthBar.SetScale(health / maxHealth);

        if (health <= 0)
        {
            SpawnEffect(particle, transform.position, particle.transform.rotation);

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                SpawnEnemy(child, spawnPoints[i]);
            }
            Spawner.AddActiveEnemies(spawnPoints.Length);

            Pools.ReturnToPool(gameObject.name, gameObject);

            MainTower.AddEnergy(reward);
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

    private void SpawnEnemy(GameObject enemy, Transform spawnPoint)
    {
        GameObject newEnemy = Pools.GetFromPool(enemy.name, enemy);
        newEnemy.name = enemy.name;
        newEnemy.transform.position = spawnPoint.position;
        newEnemy.transform.rotation = spawnPoint.rotation;

        newEnemy.GetComponent<Enemy>().SetLevel(level);

        //Zabezpečí, že child bude pokračovať na ceste, tam kde parent skončil
        FollowPath followPath = newEnemy.GetComponent<FollowPath>();
        followPath.SetWayPointIndex(gameObject.GetComponent<FollowPath>().GetWayPointIndex());

        newEnemy.SetActive(true);
    }
}

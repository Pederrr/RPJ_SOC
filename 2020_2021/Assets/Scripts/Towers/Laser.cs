using System.Collections.Generic;
using UnityEngine;

public class Laser : Tower
{
    [SerializeField] private LineRenderer[] lasers;
    [SerializeField] private float damageOverTime = 0.1f;
    [SerializeField] private float slowPercentage = 20f;

    private List<Enemy> enemyScripts = new List<Enemy>();

    public override void AddToEnemyList(Transform newTransform)
    {
        base.AddToEnemyList(newTransform);
        enemyScripts.Add(newTransform.GetComponent<Enemy>());
    }

    public override void RemoveFromEnemyList(Transform newTransform)
    {
        base.RemoveFromEnemyList(newTransform);
        enemyScripts.Remove(newTransform.GetComponent<Enemy>());
    }

    protected override void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        startRadius = _collider.radius;

        stats.Add("Level", level.ToString());
        stats.Add("Range", startRadius.ToString());

        stats.Add("Active Lasers", level.ToString());
        stats.Add("Damage Over Time", damageOverTime.ToString());
        stats.Add("Slow Percentage", slowPercentage.ToString());
    }

    protected override void FindNewTarget()
    {
        base.FindNewTarget();
        if (target == null)
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                if (lasers[i].enabled)
                {
                    lasers[i].enabled = false;
                }
            }
        }
    }

    protected override void Update()
    {
        if (target != null)
        {
            LaserShoot();
        }
        UpdateOtherThings();
    }

    private void LaserShoot()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            if (lasers[i].enabled)
            {
                lasers[i].enabled = false;
            }
        }
        int length = enemyScripts.Count;
        if (length > level)
        {
            length = level;
        }

        if (length > lasers.Length)
        {
            length = lasers.Length;
        }

        for (int i = 0; i < length; i++)
        {
            if (!lasers[i].enabled)
            {
                lasers[i].enabled = true;
            }
            lasers[i].SetPosition(0, transform.position);
            lasers[i].SetPosition(1, enemyScripts[i].transform.position);
            enemyScripts[i].GetHit(damageOverTime * level * Time.deltaTime);
            enemyScripts[i].Slow(1 - (CalculateSlowPercentage()/100));
        }
    }

    public override Dictionary<string, string> GetStats()
    {
        stats["Level"] = level.ToString();
        stats["Range"] = radius.ToString();

        stats["Active Lasers"] = level.ToString();
        stats["Damage Over Time"] = (damageOverTime * level).ToString();
        stats["Slow Percentage"] = CalculateSlowPercentage().ToString();
        return stats;
    }

    private float CalculateSlowPercentage()
    {
        return slowPercentage + (level * 2);
    }
}

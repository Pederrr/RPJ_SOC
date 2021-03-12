using System.Collections.Generic;
using UnityEngine;

public class MiniGun : Tower
{
    [SerializeField] private int burstSize = 18;
    private int burstIndex = 0;
    private float startBurstTimer = 0.05f; 
    private bool inBurst = false;
    private float burstTimer;
    private float noBurstTime = 0; //Ak sa veža nachádza v burste, ale nestrieľa, pretože nemá target

    protected override float CalculateDMG()
    {
        return 10 + (level * 2);
    }

    protected override void Awake()
    {
        base.Awake();
        stats.Add("Burst size", CalculateBurstSize().ToString());
    }

    private int CalculateBurstSize()
    {
        return burstSize + (level * 2);
    }

    protected override void Update()
    {
        if (target != null)
        {
            Aim();
            if (timer <= 0 && canShoot && !inBurst)
            {
                inBurst = true;
                burstIndex = 0;
                burstTimer = startBurstTimer;
            }

            if (inBurst)
            {
                if (burstTimer > 0)
                {
                    burstTimer -= Time.deltaTime;
                }
                
                if (burstIndex < CalculateBurstSize())
                {
                    if (burstTimer <= 0)
                    {
                        burstTimer = startBurstTimer;
                        burstIndex++;
                        Debug.Log(burstIndex);
                        Shoot();
                    }
                }
                else
                {
                    inBurst = false;
                    timer = reloadTime;
                }
            }
        }
        else
        {
            if (inBurst)  //ak bude v burste a nebude mat nepriatela, po nejakom case sa "reloadne"
            {
                noBurstTime += Time.deltaTime;
                if (noBurstTime >= 2)
                {
                    inBurst = false;
                    noBurstTime = 0;
                }
            }
            Quaternion newRotation = Quaternion.Lerp(firePoint.transform.rotation, startRotation, Time.deltaTime * turnSpeed);
            firePoint.transform.rotation = newRotation;
        }

        UpdateOtherThings();
    }

    protected override void Shoot()
    {
        //AudioManager.Instance.Play(sfxName, true); //zmeniť zvuk

        Quaternion rotation = firePoint.rotation;
        rotation *= Quaternion.Euler(0, 0, Random.Range(-10, 10)); //bullet spread

        SpawnBullet(bulletPrefab, firePoint.position, rotation);
        newRB2D = newBullet.GetComponent<Rigidbody2D>();
        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.SetDamage(CalculateDMG());
        bullet.SetTower(transform);
        bullet.SetRadiusOfTower(_collider.radius);

        newRB2D.AddForce(bullet.transform.up * -1f * force, ForceMode2D.Impulse);
    }

    public override Dictionary<string, string> GetStats()
    {
        stats["Level"] = level.ToString();
        stats["Range"] = radius.ToString();

        stats["Damage"] = CalculateDMG().ToString();

        stats["Burst size"] = CalculateBurstSize().ToString();

        return stats;
    }
}

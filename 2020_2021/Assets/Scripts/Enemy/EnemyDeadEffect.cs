using System.Collections;
using UnityEngine;

public class EnemyDeadEffect : MonoBehaviour
{
    private float time;
    private void OnEnable()
    {
        time = GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        StartCoroutine(Despawn(time));
    }

    private IEnumerator Despawn(float time)
    { 
        yield return new WaitForSeconds(time);
        Pools.ReturnToPool(gameObject.name, gameObject);
    }
}

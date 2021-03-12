using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private int[] count;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float[] rate;
    [SerializeField] private float timeToNextWave;

    public int[] GetCount()
    {
        return count;
    }

    public float[] GetRate()
    {
        return rate;
    }

    public GameObject[] GetEnemies()
    {
        return enemies;
    }

    public float GetTimeToNextWave()
    {
        return timeToNextWave;
    }
}

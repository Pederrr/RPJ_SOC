using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] protected int towerIndex;
    [SerializeField] protected int level = 1;
    
    public void Upgrade()
    {
        level++;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetTowerIndex()
    {
        return towerIndex;
    }
}

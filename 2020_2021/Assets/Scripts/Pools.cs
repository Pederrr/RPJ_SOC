using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int quantity;

        public string GetName()
        {
            return prefab.name;
        }

        public GameObject GetPrefab()
        {
            return prefab;
        }

        public int GetQuantity()
        {
            return quantity;
        }
    }

    [SerializeField] private Transform parentObj;
    private static Transform parentObject;

    [SerializeField] private List<Pool> pools;
    
    private static Dictionary<string, Queue<GameObject>> objectPools;

    private void Start()
    {
        parentObject = parentObj;
        objectPools = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            objectPools.Add(pool.GetName(), new Queue<GameObject>());

            for (int i = 0; i < pool.GetQuantity(); i++)
            {
                GameObject obj = Instantiate(pool.GetPrefab());
                obj.name = pool.GetName(); //aby nebolo meno prefab(clone)
                obj.transform.SetParent(parentObject);
                obj.SetActive(false);
                objectPools[pool.GetName()].Enqueue(obj);
            }
        }
    }

    public static GameObject GetFromPool(string name,GameObject prefab)
    {
        if (!objectPools.ContainsKey(name)) //nemusím cez inspector pridávať objekty na pool, keď chcem niečo poolovať, tak si vytvorím pool
        {
            objectPools.Add(name, new Queue<GameObject>());
            //Debug.Log("creating pool" + name);
        }

        if (objectPools[name].Count == 0)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(parentObject);
            obj.SetActive(false);
            objectPools[name].Enqueue(obj);
        }
        return objectPools[name].Dequeue();
    }

    public static void ReturnToPool(string name, GameObject objectToReturn)
    {
        if (!objectPools.ContainsKey(name))
        {
            objectPools.Add(name, new Queue<GameObject>());
            //Debug.Log("creating pool" + name);
        }

        objectToReturn.SetActive(false);
        objectPools[name].Enqueue(objectToReturn);
    }
}

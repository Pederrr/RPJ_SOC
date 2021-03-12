using UnityEngine;

public class FarmTower : Building
{
    [SerializeField] private bool RAM;
    [SerializeField] private bool CPU;
    private float time;

    private GameObject areaObj;
 
    private void OnEnable()
    {
        areaObj = transform.GetChild(1).gameObject;
        level = 1;
        time = 1f;

        if (RAM)
        {
            InvokeRepeating("GenerateRAM", time, time);
        }
        else if (CPU)
        {
            InvokeRepeating("GenerateCPU", time, time);
        }
    }

    private void OnDisable()
    {
        CancelInvoke(); //zruší invokeRepeating ktorá zapnem keď vežu postavím
    }

    private void GenerateRAM()
    {     
        MainTower.AddRAM(level);
    }

    private void GenerateCPU()
    {
        MainTower.AddCPU(level);
    }

    private void Update()
    {
        if (GameManager.GetSelectedObject() == gameObject)
        {
            if (!areaObj.activeSelf)
            {
                areaObj.SetActive(true);
            }
        }
        else
        {
            areaObj.SetActive(false);
        }
    }
}

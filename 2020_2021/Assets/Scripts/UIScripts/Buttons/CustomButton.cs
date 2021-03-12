using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomButton : MonoBehaviour
{
    protected GameObject square;
    protected Square scriptSq;

    [SerializeField] protected int towerIndex;

    [SerializeField] protected TextMeshProUGUI descriptionTextUI;
    [SerializeField] protected TextMeshProUGUI priceTextUI;

    private void OnEnable()
    {
        descriptionTextUI.text = "";
        priceTextUI.text = "";
    }

    public int GetTowerIndex()
    {
        return towerIndex;
    }

    protected List<int> GetNewPrice(int towerIndex, int level)
    {
        List<int> localPrice = new List<int>();
        if (level >= 5)
        {
            //Ak chcem násobiť údaje v liste, musím si vytvoriť úplne novú premennú - nie iba link na ten list
            int[] veryLocalPrice = Shop.GetPrice(towerIndex, 4);
            for (int i = 0; i < veryLocalPrice.Length; i++)
            {
                localPrice.Add(veryLocalPrice[i]);
            }

            for (int i = 0; i < localPrice.Count; i++)
            {
                localPrice[i] = localPrice[i] + level * 10; //treba doladiť
            }
        }
        else
        {
            localPrice = new List<int>(Shop.GetPrice(towerIndex, level));
        }
        return localPrice;
    }
}


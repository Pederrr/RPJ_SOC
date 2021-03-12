using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : CustomButton, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private GameObject upgrading;
    private Building upgradingScript;

    private List<int> price;

    public int GetLevel()
    {
        upgrading = GameManager.GetSelectedObject();
        upgradingScript = upgrading.GetComponent<Building>();
        int level = upgradingScript.GetLevel();
        return level;
    }

    public void Click()//OnMouseDown
    {
        upgrading = GameManager.GetSelectedObject();

        upgradingScript = upgrading.GetComponent<Building>();

        towerIndex = upgradingScript.GetTowerIndex();

        int level = upgradingScript.GetLevel();

        if (level < Shop.GetMaxLevel())
        {
            price = GetNewPrice(towerIndex, level);

            if (MainTower.ShowEnergy() >= price[0] && MainTower.ShowRAM() >= price[1] && MainTower.ShowCPU() >= price[2])
            {
                upgradingScript.Upgrade();
                MainTower.SubtractEnergy(price[0]);
                MainTower.SubtractRAM(price[1]);
                MainTower.SubtractCPU(price[2]);
            }
            else
            {
                Debug.Log("Not enough resources!");

            }

            //GameManager.SetActiveUI(null);
        }
        else
        {
            GameObject tut = GameObject.Find("TutorialManager"); //poriešiť inak
            if (tut != null)
            {
                tut.GetComponent<Tutorial>().PanelClick();
            } 
            Debug.Log("Max level reached!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RefreshUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionTextUI.text = "";
        priceTextUI.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        GameObject selected = GameManager.GetSelectedObject();

        int level = selected.GetComponent<Building>().GetLevel();

        if (level >= Shop.GetMaxLevel())
        {
            descriptionTextUI.text = "Current max level for towers was reached";
        }
        else
        {
            List<int> localPrice = GetNewPrice(towerIndex, level);

            descriptionTextUI.text = "This tower will be upgraded";
            priceTextUI.text =  "Price: " + "\n" + localPrice[0] + " Energy" + "\n" + localPrice[1] + " CPU" + "\n" + localPrice[2] + "RAM";
        }
    }
}

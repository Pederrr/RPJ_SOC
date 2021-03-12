using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellButton : CustomButton, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject selling;
    private Building sellingScript;

    private List<int> price;

    public int GetLevel()
    {
        selling = GameManager.GetSelectedObject();
        sellingScript = selling.GetComponent<Building>();
        int level = sellingScript.GetLevel() - 1;
        return level;
    }

    public void Click()//OnMouseDown
    {
        selling = GameManager.GetSelectedObject(); 
        sellingScript = selling.GetComponent<Building>();
        towerIndex = sellingScript.GetTowerIndex();

        sellingScript = selling.GetComponent<Building>();

        price = GetNewPrice(towerIndex, sellingScript.GetLevel() - 1);

        MainTower.AddEnergy(price[0] / 2);
        MainTower.AddRAM(price[1] / 2);
        MainTower.AddCPU(price[2] / 2);

        GameManager.SetSelectedObject(null);
        GameManager.SetActiveUI(null);

        //Destroy(selling);
        Pools.ReturnToPool(selling.name, selling);

        //scriptSq.SetBuildOnTop(null);
        Debug.Log("Sold");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject selected = GameManager.GetSelectedObject();
        
        int level = selected.GetComponent<Building>().GetLevel() - 1;

        List<int> price = GetNewPrice(towerIndex, level);

        descriptionTextUI.text = "This tower will be destroyed";
        priceTextUI.text = "You will get: " + "\n" + price[0]/2 + " Energy" + "\n" + price[1]/2 + " CPU" + "\n" + price[2]/2 + " RAM";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionTextUI.text = "";
        priceTextUI.text = "";
    }
}

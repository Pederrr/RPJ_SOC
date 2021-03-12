using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildButton : CustomButton, IPointerEnterHandler, IPointerExitHandler
{
    private int[] price;
    private Image button;

    private void Awake()
    {
        button = GetComponent<Image>();
    }

    private void OnEnable()
    {
        Color col = button.color;
        if (!Shop.IsUnlocked(towerIndex))
        {
            col.a = 0.14f;
            button.color = col;
        }
        else
        {
            col.a = 1f;
            button.color = col;
        }
    }

    private void OnDisable()
    {
        descriptionTextUI.text = "";
        priceTextUI.text = "";
    }

    public void Click() //OnMouseDownPovodne
    {
        square = GameManager.GetSelectedObject();
        scriptSq = square.GetComponent<Square>();

        if (Shop.IsUnlocked(towerIndex))
        {
            price = Shop.GetPrice(towerIndex, 0);

            if (MainTower.ShowEnergy() >= price[0] && MainTower.ShowRAM() >= price[1] && MainTower.ShowCPU() >= price[2])
            {
                MainTower.SubtractEnergy(price[0]);
                MainTower.SubtractRAM(price[1]);
                MainTower.SubtractCPU(price[2]);
                    
                    
                //GameObject newBuild = Instantiate(Shop.GetFromTowers(index), square.transform.position, square.transform.rotation);
                GameObject newBuild = SpawnBuilding(Shop.GetFromTowers(towerIndex), new Vector3(square.transform.position.x, square.transform.position.y, -1), square.transform.rotation);
                //'z' je -1 preto, aby pri klikaní na vežu, ktorú postavím, vždy zachatil ju
                    
                    
                GameManager.SetSelectedObject(null);
                GameManager.SetActiveUI(null);
            }
            else
            {
                Debug.Log("Not enough resources!");
            }
        }
        else
        {
            Debug.Log("You need to unlock that!");
        }
    }

    private GameObject SpawnBuilding(GameObject building, Vector3 position, Quaternion rotation)
    {
        GameObject newBuild = Pools.GetFromPool(building.name, building);
        newBuild.name = building.name;
        newBuild.transform.position = position;
        newBuild.transform.rotation = rotation;
        newBuild.SetActive(true);
        return newBuild;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject selected = GameManager.GetSelectedObject();
        int level = 0;
        if (selected.CompareTag("tower"))
        {
            level = selected.GetComponent<Building>().GetLevel();
        }

        int[] price = Shop.GetPrice(towerIndex, level);

        descriptionTextUI.text = Shop.GetDesription(towerIndex);
        priceTextUI.text = "Price: " + "\n" + price[0] + " Energy" + "\n" +  price[1] + " CPU" + "\n" + price[2] + " RAM";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionTextUI.text = "";
        priceTextUI.text = "";
    }
}

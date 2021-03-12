using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MainTowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI description;

    private void Start()
    {
        description.text = "";
        price.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RefreshUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.text = "";
        price.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        int level = MainTower.GetLevel();
        if (level < MainTower.GetMaxLevel())
        {
            description.text =
                "Current level: " + level + "\n" +
                "You will get + 2 HP \n" +
                "Max level of all towers will be increased";
            price.text = "Price: " + "\n" + level * 50 + "$" + "\n" + level * 50 + "Pla" + "\n" + level * 50 + "Mic";
        }
        else
        {
            description.text = "Current level: " + level + "\n" + "Max level was reached";
        }
    }
}

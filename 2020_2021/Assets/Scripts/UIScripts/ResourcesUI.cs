using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI money;
    [SerializeField] private TMPro.TextMeshProUGUI plastic;
    [SerializeField] private TMPro.TextMeshProUGUI microChips;

    private void Update()
    {
        money.text = "Energy: " + MainTower.ShowEnergy();
        plastic.text = "CPU: " + MainTower.ShowCPU();
        microChips.text = "RAM: " + MainTower.ShowRAM();
    }  
}

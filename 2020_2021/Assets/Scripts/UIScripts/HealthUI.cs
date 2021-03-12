using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;

    private void Update()
    {
        text.text = "HEALTH: " + MainTower.ShowHealth();
    }
}

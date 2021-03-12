using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wave;
    [SerializeField] private TextMeshProUGUI time;
    void Update()
    {
        wave.text = "WAVE: " + MainTower.GetWave();
        time.text =  Spawner.GetTimeToNextWave().ToString("F1");
    }
}

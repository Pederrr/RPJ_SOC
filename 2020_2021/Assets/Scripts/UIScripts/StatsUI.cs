using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI stats;

    private GameObject selected;

    private void Start()
    {
        StartCoroutine(UpdateUI());
    }

    private IEnumerator UpdateUI()
    {
        while (true)
        {
            selected = GameManager.GetSelectedObject();

            if (selected == null)
            {
                stats.enabled = false;
            }
            else if (selected.CompareTag("square"))
            {
                stats.text = "Choose what you want to build";
                stats.enabled = true;
            }
            else if (selected.CompareTag("tower"))
            {
                Dictionary<string, string> towerStats = selected.GetComponent<Tower>().GetStats();
                List<string> keys = new List<string>(towerStats.Keys);

                string finalString = selected.name + "\n";

                foreach (string key in keys)
                {
                    finalString += key + ": " + towerStats[key] + "\n";
                }

                stats.text = finalString;

                stats.enabled = true;
            }
            else if (selected.CompareTag("farm"))
            {
                FarmTower farm = selected.GetComponent<FarmTower>();
                stats.text = "Name: " + selected.name + "\n" +
                             "Level: " + farm.GetLevel();

                stats.enabled = true;
            }
            else if (selected.CompareTag("enemy"))
            {
                Enemy enemy = selected.GetComponent<Enemy>();

                stats.text =
                    enemy.gameObject.name + "\n" +
                    "Health: " + enemy.GetHealth().ToString("F0") + "\n" +
                    "Level: " + enemy.GetLevel() + "\n" +
                    "Speed: " + enemy.GetSpeed() + "\n" +
                    "Damage: " + enemy.GetDamage() + "\n" +
                    "Reward: " + enemy.GetReward() + "\n";

                stats.enabled = true;
            }
            else
            {
                stats.enabled = false;
            }
            yield return new WaitForSecondsRealtime(.05f); //Toto zabezpečí, že program sa bude opakovať každých 0.05 sekundy
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class UnlockedLevels : MonoBehaviour
{
    private List<GameObject> buttons = new List<GameObject>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            buttons.Add(child.gameObject);
        }

        foreach (GameObject button in buttons)
        {
            int unlocked = PlayerPrefs.GetInt("CompletedLevels", 0);
            for (int i = 0; i < unlocked + 1; i++)
            {
                buttons[i].GetComponent<LevelSelectButton>().Unlock();
            }
        }
    }
}

using UnityEngine;

public class LevelSelectWindow : MonoBehaviour
{
    private GameObject scanUI;
    private GameObject startGameUI;
    private GameObject levelSelectUI;

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            switch (child.name)
            {
                case "ScanUI":
                    scanUI = child.gameObject;
                    break;
                case "StartGameUI":
                    startGameUI = child.gameObject;
                    break;
                case "LevelSelect":
                    levelSelectUI = child.gameObject;
                    break;
                default:
                    break;
            }
        }

        scanUI.SetActive(true);
        startGameUI.SetActive(false);
        levelSelectUI.SetActive(false);
    }
}

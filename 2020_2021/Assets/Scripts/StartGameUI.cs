using UnityEngine;
using TMPro;

public class StartGameUI : MonoBehaviour
{
    [SerializeField] private GameObject endlessPlayButton;
    [SerializeField] private GameObject levelPlayButton;
    [SerializeField] private TextMeshProUGUI highScore;

    private void OnEnable()
    {
        int tutorialComplete = PlayerPrefs.GetInt("tutorial", 0);
        if (tutorialComplete == 0)
        {
            //endlessPlayButton.SetActive(false); 
            //levelPlayButton.SetActive(false);
        }
        else
        {
            endlessPlayButton.SetActive(true);
            levelPlayButton.SetActive(true);
        }

        highScore.text = (PlayerPrefs.GetInt("highScore", 0)).ToString();
    }
}

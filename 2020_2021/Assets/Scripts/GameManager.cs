using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameObject activeUI;

    private static GameObject selectedObject;

    [SerializeField] private int levelID;
    private static int levelIDStatic;

    [SerializeField] private GameObject buildUI;
    [SerializeField] private GameObject upgradeSellUI;
    [SerializeField] private GameObject levelWonUI;
    private static GameObject levelWonUIstatic;
    private static GameObject build;
    private static GameObject upgradeSell;

    public static bool levelWon;

    //private static GameManager instance;

    private void Awake()
    {
        levelIDStatic = levelID;
        Time.timeScale = 1;
        //instance = this;
        levelWon = false;
        activeUI = null;
        Globals.LastScene.name = SceneManager.GetActiveScene().name;
        Globals.LastScene.index = SceneManager.GetActiveScene().buildIndex;
        upgradeSell = upgradeSellUI;
        build = buildUI;
        selectedObject = null;
        activeUI = null;
        levelWonUIstatic = levelWonUI;
    }

    public void Pause() 
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        
    }

    public static void SetActiveUI(GameObject ui)
    {
        if (activeUI != null)
        {
            if (ui == null)
            {
                activeUI.SetActive(false);
                activeUI = null;
            }
            else if (activeUI == ui)
            {
                SetSelectedObject(selectedObject);
            }
            else
            {
                activeUI.SetActive(false);
                activeUI = ui;
                activeUI.SetActive(true);
            }
        }
        else if (ui != null)
        {
            activeUI = ui;
            activeUI.SetActive(true);
        }
    }

    public static GameObject GetActiveUI()
    {
        return activeUI;
    } 

    public static void SetSelectedObject(GameObject _object)
    {
        selectedObject = _object;
    }

    public static GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    public static void Win(int waveIndex)
    {
        int highscore = PlayerPrefs.GetInt("highScore", 0);
        if (waveIndex > highscore)
        {
            PlayerPrefs.SetInt("highScore", waveIndex);
        }

        int completedLevels = PlayerPrefs.GetInt("CompletedLevels", 0);
        if (levelIDStatic> completedLevels)
        {
            PlayerPrefs.SetInt("CompletedLevels", levelIDStatic);
        }

        levelWonUIstatic.SetActive(true);
    }

    public static GameObject GetBuildUI()
    {
        return build;
    }

    public static GameObject GetUpgradeSellUI()
    {
        return upgradeSell;
    }
}

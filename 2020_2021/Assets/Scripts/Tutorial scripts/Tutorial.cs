using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject endUI;
    private int panelIndex;
    private float timer;
    private GameObject selected;
    private GameObject activeUI;

    [SerializeField] private GameObject pausePanel;
    private bool hasFocus = true;

    public void PanelClick()
    {
        if (panelIndex == 6)
        {
            Time.timeScale = 1;
        }
        panelIndex++;
       
    }

    public void EndWave()
    {
        panelIndex++;
    }

    public void EndTutorial()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        endUI.SetActive(true);
    }

    private void Awake()
    {
        panelIndex = 0;
        timer = 7f;
        endUI.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnApplicationFocus(bool focus)
    {
        hasFocus = focus;
    }

    private void Update()
    {
        if (!hasFocus && !pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);
        }
        else if (hasFocus && pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }

        for (int i = 0; i < panels.Length; i++)
        {
            if (i <= panels.Length - 1)
            {
                if (i == panelIndex)
                {
                    panels[i].SetActive(true);
                }
                else
                {
                    panels[i].SetActive(false);
                }
            }
        }

        switch (panelIndex)
        {
            case 1:
                selected = GameManager.GetSelectedObject();

                if (selected != null)
                {
                    if (selected.CompareTag("square"))
                    {
                        panelIndex++;
                    }
                }
                break;

            case 2:
                activeUI = GameManager.GetActiveUI();

                if (activeUI == null)
                {
                    panelIndex++;
                }
                break;

            case 3:
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                break;

            case 4:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Time.timeScale = 0;
                    panelIndex++;
                    timer = 0.5f;
                }
                break;

            case 5:
                selected = GameManager.GetSelectedObject();
                if (selected != null)
                {
                    if (selected.CompareTag("enemy"))
                    {
                        panelIndex++;
                    }
                }
                break;

            case 7:
                if (Spawner.GetActiveEnemies() == 0)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        Time.timeScale = 0;
                        panelIndex++;
                    }
                }
                break;

            case 8:
                selected = GameManager.GetSelectedObject();
                if (selected != null)
                {
                    if (selected.CompareTag("tower"))
                    {
                        panelIndex++;
                    }
                }
                break;

            case 9:
                activeUI = GameManager.GetActiveUI();
                if (activeUI == null)
                {
                    panelIndex++;
                }
                break;

            case 10:
                activeUI = GameManager.GetActiveUI();
                if (activeUI == null)
                {
                    panelIndex++;
                }
                break;

            case 11:
                selected = GameManager.GetSelectedObject();
                if (selected != null)
                {
                    if (selected.CompareTag("tower"))
                    {
                        panelIndex++;
                    }
                }
                break;

            case 12:
                selected = GameManager.GetSelectedObject();
                int level = selected.GetComponent<Building>().GetLevel();
                if (level >= 2)
                {
                    panelIndex++;
                }
                break;

            case 13:
                selected = GameManager.GetSelectedObject();
                if (selected != null)
                {
                    if (selected.CompareTag("square"))
                    {
                        panelIndex++;
                    }
                }
                break;

            case 14:
                activeUI = GameManager.GetActiveUI();
                if (activeUI == null)
                {
                    Time.timeScale = 1;
                    panelIndex++;
                    timer = 20;
                }
                break;

            case 15:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else 
                {
                    Time.timeScale = 0;
                }

                selected = GameManager.GetSelectedObject();
                if (selected != null)
                {
                    if (selected.CompareTag("square"))
                    {
                        Time.timeScale = 0;
                        panelIndex++;
                    }
                }
                break;

            case 16:
                activeUI = GameManager.GetActiveUI();
                if (activeUI == null)
                {
                    Time.timeScale = 1;
                    panelIndex++;
                }
                break;
        }
    }
}

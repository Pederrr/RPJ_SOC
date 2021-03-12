using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private bool click;
    private GameObject lastWindowToOpen;

    [SerializeField] private GameObject levelSelectWindow;
    [SerializeField] private GameObject optionsWindow;
    [SerializeField] private GameObject myComputerWindow;
    [SerializeField] private GameObject startMenu;

    public void SetClick(bool _click)
    {
        click = _click;
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        click = false;
    }

    public void OpenWindowDoubleClick(GameObject window)
    {
        if (startMenu.activeSelf)
        {
            startMenu.SetActive(false);
        }

        if (lastWindowToOpen != window) //aby som zabezpečil, že musím kliknúť dva krát na tú istú ikonku
        {
            click = false;
        }

        lastWindowToOpen = window;

        if (!click)
        {
            click = true;
        }
        else
        {
            if (levelSelectWindow.activeSelf)
            {
                levelSelectWindow.SetActive(false);
            }

            if (optionsWindow.activeSelf)
            {
                optionsWindow.SetActive(false);
            }

            if (myComputerWindow.activeSelf)
            {
                myComputerWindow.SetActive(false);
            }

            if (window != null)
            {
                window.SetActive(true);
            }

            click = false;
        }
    }

    public void OpenWindowSingleClick(GameObject window)
    {
        if (levelSelectWindow.activeSelf)
        {
            levelSelectWindow.SetActive(false);
        }

        if (optionsWindow.activeSelf)
        {
            optionsWindow.SetActive(false);
        }

        if (myComputerWindow.activeSelf)
        {
            myComputerWindow.SetActive(false);
        }

        if (window != null)
        {
            window.SetActive(true);
        }
    }
}

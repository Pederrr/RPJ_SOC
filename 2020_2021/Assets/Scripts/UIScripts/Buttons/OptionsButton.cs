using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectWindow;
    [SerializeField] private GameObject myCoputerWindow;

    public void CloseOtherWindows()
    {
        if (levelSelectWindow.activeSelf)
        {
            levelSelectWindow.SetActive(false);
        }

        if (myCoputerWindow.activeSelf)
        {
            myCoputerWindow.SetActive(false);
        }
    }
}

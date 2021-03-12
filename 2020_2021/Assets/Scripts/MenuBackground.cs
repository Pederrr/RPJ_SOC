using UnityEngine;
using UnityEngine.EventSystems;

public class MenuBackground : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private MainMenuUI mainMenuUI;


    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            startMenu.SetActive(false);
            mainMenuUI.SetClick(false);
        }
    }
}

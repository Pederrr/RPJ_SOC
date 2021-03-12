using UnityEngine;
using UnityEngine.EventSystems;

public class LevelBackground : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.SetActiveUI(null);
            GameManager.SetSelectedObject(null);

            if (startMenu.activeSelf)
            {
                startMenu.SetActive(false);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableTower : MonoBehaviour
{
    private GameObject upgradeSell;

    private void Start()
    {
        upgradeSell = GameManager.GetUpgradeSellUI();
    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.SetSelectedObject(transform.parent.gameObject);
            GameManager.SetActiveUI(upgradeSell);
            upgradeSell.SetActive(true);
        }
    }
}

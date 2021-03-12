using UnityEngine;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour
{
    [SerializeField] private GameObject hooverText;

    private int towerIndex;

    private GameObject build;

    public int GetTowerIndex()
    {
        return towerIndex;
    }

    public void SetTowerIndex(int index)
    {
        towerIndex = index;
    }

    private void Start()
    {
        build = GameManager.GetBuildUI();
    }

    public void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (GameManager.GetSelectedObject() == null)
            {
                if (!hooverText.activeSelf)
                {
                    hooverText.SetActive(true);
                }
            }
            else if (hooverText.activeSelf)
            {
                hooverText.SetActive(false);
            }
        }
    }

    public void OnMouseExit()
    {
        hooverText.SetActive(false);
    }

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.SetSelectedObject(gameObject);
            GameManager.SetActiveUI(build);
        }
    }
}

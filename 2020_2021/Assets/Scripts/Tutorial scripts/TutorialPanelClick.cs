using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPanelClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Tutorial tutorial;
    public void OnPointerDown(PointerEventData eventData)
    {
        tutorial.PanelClick();
    }
}

using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject startMenu; 

    public void Click()
    {
        startMenu.SetActive(!startMenu.activeSelf); //ak je zapnuty, tak ho vypne a naopak
    }
}

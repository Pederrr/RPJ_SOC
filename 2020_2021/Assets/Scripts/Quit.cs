using UnityEngine;

public class Quit : MonoBehaviour
{
    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}

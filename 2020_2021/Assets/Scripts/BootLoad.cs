using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoad : MonoBehaviour
{
    private void Start()
    {
        Invoke("LoadMainMenu", 3f);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

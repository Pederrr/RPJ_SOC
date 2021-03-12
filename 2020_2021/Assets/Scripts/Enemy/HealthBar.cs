using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform bar;

    public void SetScale(float normalizedScale)
    {
        if (normalizedScale != 1)
        {
            gameObject.SetActive(true);
            bar.localScale = new Vector3(normalizedScale, 1, 0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

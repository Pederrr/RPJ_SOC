using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI[] texts = new TextMeshProUGUI[2];
    private void Awake()
    {
        button = GetComponent<Button>();
        texts[0] = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        texts[1] = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void Unlock()
    {
        button.interactable = true;
        foreach (TextMeshProUGUI text in texts)
        {
            Color color = text.color;
            color.a = 1;
            text.color = color;
        }
    }
}

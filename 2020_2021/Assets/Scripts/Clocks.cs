using UnityEngine;
using TMPro;
using System;

public class Clocks : MonoBehaviour
{
    private TextMeshProUGUI clocks;

    private void Start()
    {
        clocks = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        clocks.text =
            DateTime.Now.ToString("HH:mm") + "\n" +
            DateTime.Now.Date.ToString("dd.MM.yyyy");
    }
}

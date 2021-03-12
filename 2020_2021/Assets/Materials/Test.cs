using UnityEngine; //Import potrebných knižníc

public class Test : MonoBehaviour
{
    private int cislo = 1; //Definovanie premmených

    private void Start()
    {
        //Výpis do konzoly
        Debug.Log("Toto sa stane len a začiatku");
    }

    private void Update()
    {
        Debug.Log("Toto sa bude opakovať s každou snímkou");
    }
}

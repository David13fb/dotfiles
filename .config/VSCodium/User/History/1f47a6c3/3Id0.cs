using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmoothUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ject.GetComponent<TMPro.TMP_InputField>().onValueChanged.AddListener(delegate { 
    UCM.IAV.Movimiento.GameManager.instance.SetSmooth(ject.GetComponent<TMPro.TMP_InputField>().text); 
});

    }

  

}

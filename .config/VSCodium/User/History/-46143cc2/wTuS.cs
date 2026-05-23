using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; 
public class TimeUIController : MonoBehaviour
{
    TextMeshProUGUI _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "Tiempo de la partida = " +  DataResManager.Instance.GetGameTime() + "s";
    }

    // Update is called once per frame
    void Update()
    {
    if (Keyboard.current.uKey.isPressed)
    {
        float act = DataResManager.Instance.GetGameTime() + 1;
        DataResManager.Instance.SetGameTime(act);
        _text.text = "Tiempo de la partida = " + act + "s";
    }

    if (Keyboard.current.iKey.isPressed)
    {
        float act = DataResManager.Instance.GetGameTime() - 1;
        DataResManager.Instance.SetGameTime(act);
        _text.text = "Tiempo de la partida = " + act + "s";
    }
    }
}

using TMPro;
using UnityEngine;

public class TimeUIController : MonoBehaviour
{
    TextMeshPro _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
        _text.text = "Tiempo de la partida = " +  DataResManager.Instance.GetGameTime() + "s";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            float act =  DataResManager.Instance.GetGameTime()+1;
            DataResManager.Instance.SetGameTime(act);
            _text.text = "Tiempo de la partida = " + act + "s";
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            float act =  DataResManager.Instance.GetGameTime()-1;
            DataResManager.Instance.SetGameTime(act);
            _text.text = "Tiempo de la partida = " + act + "s";
        }
    }
}

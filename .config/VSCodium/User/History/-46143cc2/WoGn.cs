using TMPro;
using UnityEngine;

public class TimeUIController : MonoBehaviour
{
    TextMeshPro _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            DataResManager.Instance.GetGameTime();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            
        }
    }
}

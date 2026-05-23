using Meryel.UnityCodeAssist.Newtonsoft.Json;
using UnityEngine;

public class DataResManager : MonoBehaviour
{
    // Punto de acceso global
    public static DataResManager Instance { get; private set; }

    private float gameTime = 60.0f;
    
    public int numDeaths = 0;
    public int numkills = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
     public void SetGameTime(float t){
        gameTime = t;
    }
    public float GetGameTime(){
        numDeaths = 0;
        numkills = 0;
        return gameTime;
    }

}
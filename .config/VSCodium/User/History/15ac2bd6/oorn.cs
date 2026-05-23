using UnityEngine;

public class DataResManager : MonoBehaviour
{
    // Punto de acceso global
    public static DataResManager Instance { get; private set; }

    private float gameTime;
    public void SerGameTime(float t){
        gameTime = t;
    }
    public float GetGameTime(){
        return gameTime;
    }
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
}

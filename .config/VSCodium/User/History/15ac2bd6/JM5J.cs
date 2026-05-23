using UnityEngine;

public class DataResManager : MonoBehaviour
{
    // Punto de acceso global
    public static DataResManager Instance { get; private set; }

    private float gameTime = 60.0f;
   
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
        return gameTime;
    }

}

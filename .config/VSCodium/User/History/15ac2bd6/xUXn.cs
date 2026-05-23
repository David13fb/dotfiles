using UnityEngine;

public class DataResManager : MonoBehaviour
{
    // Punto de acceso global
    public static DataResManager Instance { get; private set; }

    private float gameTime;
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

using UnityEngine;

public class DataResManager : MonoBehaviour
{
    // Punto de acceso global
    public static DataResManager Instance { get; private set; }

    private void Awake()
    {
        // Si no hay instancia, esta es la principal
        if (Instance == null)
        {
            Instance = this;
            // Opcional: Hace que el objeto persista entre escenas
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Si ya existe otra, se destruye la copia nueva
            Destroy(gameObject);
        }
    }
}

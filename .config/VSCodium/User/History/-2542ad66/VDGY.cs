using UnityEngine;

public class paco : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform t = transform;
            t.position = UnityEngine::Vector3(0.0f,1.0f,0.0f);
    }
    
}

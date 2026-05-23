using UnityEngine;

public class ComponentePrueba : MonoBehaviour
{
    [SerializeField] private Vector3 v = Vector3.left;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += v;
    }
}

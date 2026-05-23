using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private InputActionReference _move; 
    private Transform _transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 aux = _move.action.ReadValue<Vector2>(); 
        _transform.position += new Vector3(aux.x,aux.y,0) * Time.deltaTime;
    }
}

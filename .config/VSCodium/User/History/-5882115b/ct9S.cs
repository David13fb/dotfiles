using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private InputActionReference _move; 
    [SerializeField] private InputActionReference _atk; 
    [SerializeField] private float _speed = 10.0f;

    private Animator anim;
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
        _transform.position += new Vector3(aux.x,aux.y,0)*_speed * Time.deltaTime;
    }
}

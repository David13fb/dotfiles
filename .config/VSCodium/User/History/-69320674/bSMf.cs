using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MoveCompt : MonoBehaviour
{
    private Transform _transform;
    public Patata patata;
    private Rigidbody2D _rb;
    public float _speed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A)) patata.AddPatata(1);
        _rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _speed );    
    }
}

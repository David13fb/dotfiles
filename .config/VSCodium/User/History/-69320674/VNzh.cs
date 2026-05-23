using System;
using System.Numerics;
using UnityEngine;

public class MoveCompt : MonoBehaviour
{
    private Transform _transform;
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
        _rb.AddVelocity(new Vector2(_speed,0));    
    }
}

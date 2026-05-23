using System;
using UnityEngine;

public class MoveCompt : MonoBehaviour
{
    private Transform _transform;
    public float _speed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = new Vector3(_transform.position.x+_speed* Time.DeltaTime ,0,0);    
    }
}

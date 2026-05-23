using UnityEngine;

public class DetectConeComponent : MonoBehaviour
{
    public LayerMask targetLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

     void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 10,targetLayer)){
            print("There is something in front of the object!");
        }
    }
}

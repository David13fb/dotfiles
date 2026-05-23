using UnityEngine;

public class DetectConeComponent : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _maxDistance = 10.0f;
    private float angMax = 30.0f
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

     void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 direccionOriginal = Vector3.forward;
        Quaternion rotacion = Quaternion.Euler(0,angMax, 0);
        Vector3 dirPos = rotacion * fwd;
        if (Physics.Raycast(transform.position, fwd, _maxDistance ,_targetLayer)
        ||Physics.Raycast(transform.position, dirPos, _maxDistance ,_targetLayer)||
            Physics.Raycast(transform.position,-dirPos, _maxDistance ,_targetLayer)){
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _maxDistance, Color.yellow);
            print("There is something in front of the object!");
        }
        else{
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _maxDistance, Color.white);
        }
    }
}

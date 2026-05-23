using UnityEngine;

public class DetectConeComponent : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _maxDistance = 10.0f;
    [SerializeField] private float angMax = 30.0f;
    [SerializeField] private float angPerRay = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

     void FixedUpdate()
    {
        for
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 direccionOriginal = Vector3.forward;
        Quaternion rotacion = Quaternion.Euler(0,angMax, 0);
        Vector3 dirPos = rotacion * fwd;
        rotacion = Quaternion.Euler(0,-angMax, 0);
        Vector3 dirNeg = rotacion * fwd;
        if (Physics.Raycast(transform.position, fwd, _maxDistance ,_targetLayer)
        ||Physics.Raycast(transform.position, dirPos, _maxDistance ,_targetLayer)||
            Physics.Raycast(transform.position,dirNeg, _maxDistance ,_targetLayer)){
            Debug.DrawRay(transform.position, fwd * _maxDistance, Color.yellow);
            Debug.DrawRay(transform.position, dirPos * _maxDistance, Color.yellow);
            Debug.DrawRay(transform.position, dirNeg * _maxDistance, Color.yellow);
            print("There is something in front of the object!");
        }
        else{
            Debug.DrawRay(transform.position, fwd * _maxDistance, Color.white);
            Debug.DrawRay(transform.position, dirPos * _maxDistance, Color.white);
            Debug.DrawRay(transform.position, dirNeg * _maxDistance, Color.white);
        }
    }
}

using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPRueba : MonoBehaviour
{
    [SerializeField] InputActionReference hit;
     [SerializeField] Animator manimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      hit.action.Enable();  
    }

    // Update is called once per frame
    void Update()
    {
             if(hit.action.WasPressedThisFrame()){
            manimator.SetBool("atkç",true);
        }
       else{ 
        Vector2 aux = InputSystem.actions[ "Move" ].ReadValue<Vector2>();
        transform.position += new Vector3(aux.x,aux.y,0)*Time.deltaTime;
       manimator.SetBool("atkç",false);
       }
        
    }
}

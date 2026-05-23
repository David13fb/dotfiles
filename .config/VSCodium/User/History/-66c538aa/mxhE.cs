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
        if(hit.action.IsPressed()){
            manimator.SetBool("atkç",true);
        }
        
           transform.position += new Vector3(InputSystem.actions[ "Move" ].ReadValue<Vector2>());
        manimator.SetBool("atkç",false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}

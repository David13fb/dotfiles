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
             if(hit.action.IsPressed()){
            manimator.SetBool("atkç",true);
        }
       
    }
}

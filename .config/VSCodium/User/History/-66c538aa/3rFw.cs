using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPRueba : MonoBehaviour
{
    [SerializeField] InputActionReference hit;
     [SerializeField] InputActionReference move;

     [SerializeField] AnimatorController manimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(hit.action.IsPressed()){
            manimator.parameters[0].defaultBool = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}

using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(fileName = "FindPlayerBeh", menuName = "Scriptable Objects/beh/FindPlayerBeh")]
public class FindPlayerBeh : Ibehaviour
{
    public override bool Execute(GameObject g)
    {
        TargetListControllerCmp list = g.GetComponent<TargetListControllerCmp>();
        GameObject target = list.GetTargetNPC();
        if(target != null)
        Debug.LogError(target.transform.position);
            g.GetComponent<BotGameplayActions>().TryMoveToWorldPosition(target.transform.position);
        return true;   
    }
}

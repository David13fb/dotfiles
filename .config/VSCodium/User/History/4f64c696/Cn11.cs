using Unity.FPS.Gameplay;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeWeaponBeh", menuName = "Scriptable Objects/beh/ChangeWeaponBeh")]
public class ChangeWeaponBeh : Ibehaviour
{
    [SerializeField] private bool order = false;
    public override bool Execute(GameObject g)
    {
        g.GetComponent<PlayerWeaponsManager>().SwitchWeapon(order);
        
    }
}

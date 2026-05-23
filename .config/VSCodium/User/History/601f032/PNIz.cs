using System.Collections.Generic;
using UnityEngine;

public class TargetListControllerCmp : MonoBehaviour
{
    [SerializeField] private float radOverlap = 25.0f;
    [SerializeField] private LayerMask targetLayers;

    public List<GameObject> NPCList;
    private GameObject _targetNPC;
    public List<GameObject> enemyList;
    private GameObject _targetEnemy;
    public List<GameObject> weaponList;
    private GameObject _targetWeapon;
    public List<GameObject> healthList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateLists(){
        Collider[] cols = Physics.OverlapSphere(transform.position, radOverlap, targetLayers);

        foreach(var auxcol in cols){
            if(auxcol.gameObject.layer == LayerMask.NameToLayer("Players")){
                NPCList.Add(auxcol.gameObject);
            }
            else if(auxcol.gameObject.layer == LayerMask.NameToLayer("Enemies")){
                enemyList.Add(auxcol.gameObject);
            }
            else if(auxcol.gameObject.layer == LayerMask.NameToLayer("HealthPickUp")){
                healthList.Add(auxcol.gameObject);
            }
            else if(auxcol.gameObject.layer == LayerMask.NameToLayer("WeaponPickUp")){
                weaponList.Add(auxcol.gameObject);
            }
        }
    }
    private void findBestTargets(){
        for(var aux in NPCList){

        }
        _bot.NavMeshAgent.CalculatePath()
    }
}

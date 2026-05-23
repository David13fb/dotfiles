using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.AI;

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
    private GameObject _targetHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateLists();
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
        float closestDistance = Mathf.Infinity;
        NavMeshPath path = new NavMeshPath();

        foreach(var aux in NPCList){
            _bot.NavMeshAgent.CalculatePath(aux.transform.position, path);
        }
        
    }
}


    GameObject[] potentials = GameObject.FindGameObjectsWithTag(targetTag);
        Transform bestTarget = null;
        float closestDistance = Mathf.Infinity;
        NavMeshPath path = new NavMeshPath();

        foreach (GameObject potential in potentials)
        {
            // 1. Calcular camino real sin moverse
            if (agent.CalculatePath(potential.transform.position, path))
            {
                // 2. Verificar si el camino es alcanzable
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    // 3. Calcular la longitud total del camino
                    float distance = GetPathLength(path);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        bestTarget = potential.transform;
                    }
                }
            }
        }
        return bestTarget;

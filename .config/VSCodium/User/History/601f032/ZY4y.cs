using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetListControllerCmp : MonoBehaviour
{
    [SerializeField] private readonly float radOverlap = 25.0f;
    [SerializeField] private LayerMask targetLayers;

    public List<GameObject> NPCList;
    private GameObject _targetNPC;
    public List<GameObject> enemyList;
    private GameObject _targetEnemy;
    public List<GameObject> weaponList;
    private GameObject _targetWeapon;
    public List<GameObject> healthList;
    private GameObject _targetHealth;
    private BotGameplayActions _bot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        updateLists();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void updateLists()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radOverlap, targetLayers);

        foreach (Collider auxcol in cols)
        {
            if (auxcol.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                NPCList.Add(auxcol.gameObject);
            }
            else if (auxcol.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
                enemyList.Add(auxcol.gameObject);
            }
            else if (auxcol.gameObject.layer == LayerMask.NameToLayer("HealthPickUp"))
            {
                healthList.Add(auxcol.gameObject);
            }
            else if (auxcol.gameObject.layer == LayerMask.NameToLayer("WeaponPickUp"))
            {
                weaponList.Add(auxcol.gameObject);
            }
        }
    }

    private float GetPathLength(NavMeshPath path)
    {

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            float distance = 0;
            //Calculamos distancia de la ruta calculada 
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return distance;
        }
        return float.MaxValue;
    }


    private void findBestTargets()
    {
        float closestDistance = Mathf.Infinity;
        NavMeshPath path = new();

        foreach (GameObject aux in NPCList)
        {
            _bot.NavMeshAgent.CalculatePath(aux.transform.position, path);
            if (GetPathLength(path) < closestDistance)
            {
                _targetNPC = aux;
            }
        }
        closestDistance = Mathf.Infinity;
        foreach (GameObject aux in enemyList)
        {
            _bot.NavMeshAgent.CalculatePath(aux.transform.position, path);
            if (GetPathLength(path) < closestDistance)
            {
                _targetEnemy = aux;
            }
        }
        closestDistance = Mathf.Infinity;
        foreach (GameObject aux in weaponList)
        {
            _bot.NavMeshAgent.CalculatePath(aux.transform.position, path);
            if (GetPathLength(path) < closestDistance)
            {
                _targetWeapon = aux;
            }
        }
        closestDistance = Mathf.Infinity;
        foreach (GameObject aux in healthList)
        {
            _bot.NavMeshAgent.CalculatePath(aux.transform.position, path);
            if (GetPathLength(path) < closestDistance)
            {
                _targetHealth = aux;
            }
        }
    }
}

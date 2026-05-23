using System.Collections.Generic;
using System.Diagnostics;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

public class TargetListControllerCmp : MonoBehaviour
{
    [SerializeField] private float radOverlap = 25.0f;
    [SerializeField] private float updateMiliseconds = 1.0f;
    [SerializeField] private LayerMask targetLayers;

    public List<GameObject> NPCList;
    private GameObject _targetNPC;
    float _targetNPCDis;
    public List<GameObject> enemyList;
    private GameObject _targetEnemy;
    float _targetEnemyDis;
    public List<GameObject> weaponList;
    private GameObject _targetWeapon;
    float _targetWeaponDis;
    public List<GameObject> healthList;
    private GameObject _targetHealth;
    float _targetHealthDis;

    public GameObject GetTargetNPC()=> _targetNPC;
    public GameObject GetTargetEnemy()=> _targetEnemy;
    public GameObject GetTargetWeapon()=> _targetWeapon;
    public GameObject GetTargetHealth()=> _targetHealth;

    public float GetTargetNPCDis()=> _targetNPCDis;
    public float GetTargetEnemyDis()=> _targetEnemyDis;
    public float GetTargetWeaponDis()=> _targetWeaponDis;
    public float GetTargetHealthDis()=> _targetHealthDis;
    private BotGameplayActions _bot;
    private PlayerWeaponsManager _weapons;
    private Stopwatch _stopwatch;
    private void Awake()
    {
        // Inicializamos el cron�metro aqu� para que exista antes que cualquier cosa
        _stopwatch = new Stopwatch();

        // Obtenemos las referencias
        _bot = GetComponent<BotGameplayActions>();
        _weapons = GetComponent<PlayerWeaponsManager>();
    }

    private void Start()
    {
        // Iniciamos el conteo en el Start
        if (_stopwatch != null)
        {
            _stopwatch.Start();
  
        }
    }

    // Update is called once per frame
    private void Update()
    {
      
        if (_stopwatch.ElapsedMilliseconds >= updateMiliseconds){
            updateLists();
            _stopwatch.Restart();
        }
    }

    private void updateLists()
    {
        NPCList.Clear();
        enemyList.Clear();
        healthList.Clear();
        weaponList.Clear();
        Collider[] cols = Physics.OverlapSphere(transform.position, radOverlap, targetLayers);
        foreach (Collider auxcol in cols)
        {
            if (auxcol.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                if(auxcol.gameObject!= this.gameObject && !NPCList.Contains(auxcol.gameObject))
                    NPCList.Add(auxcol.gameObject);
            }
            else if (auxcol.gameObject.layer == LayerMask.NameToLayer("Enemies") && !enemyList.Contains(auxcol.gameObject))
            {
                enemyList.Add(auxcol.gameObject);
            }
            else if (auxcol.gameObject.layer == LayerMask.NameToLayer("HealthPickUp") && !healthList.Contains(auxcol.gameObject))
            {
                healthList.Add(auxcol.gameObject);
            }
            else if (auxcol.gameObject.layer == LayerMask.NameToLayer("WeaponPickUp"))
            {
                if(_weapons.HasWeapon(auxcol.gameObject.GetComponent<WeaponController>())==null && !weaponList.Contains(auxcol.gameObject)) weaponList.Add(auxcol.gameObject);
            }
        }
        
        findBestTargets();
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
        if(_bot != null)
        {
            float closestDistance = Mathf.Infinity;
            NavMeshPath path = new();

            _targetNPCDis = closestDistance;
            _targetEnemyDis = closestDistance;
            _targetWeaponDis = closestDistance;
            _targetHealthDis = closestDistance;
            foreach (GameObject aux in NPCList)
            {
                if (aux != null)
                {
                    Debug.Log("hola uso la navmesh: " + _bot.NavMeshAgent);
                    _bot.NavMeshAgent.CalculatePath(aux.transform.position, path);
                    if (GetPathLength(path) < closestDistance)
                    {
                        _targetNPCDis = closestDistance;
                        _targetNPC = aux;
                    }
                }
            }
            closestDistance = Mathf.Infinity;
            foreach (GameObject aux in enemyList)
            {
                if (aux != null)
                {
                    _bot.NavMeshAgent.CalculatePath(aux.transform.position, path);
                    if (GetPathLength(path) < closestDistance)
                    {
                        _targetEnemyDis = closestDistance;
                        _targetEnemy = aux;
                    }
                }
            }
            closestDistance = Mathf.Infinity;
            foreach (GameObject aux in weaponList)
            {
                if (aux != null)
                {
                    4
                    if (GetPathLength(path) < closestDistance)
                    {
                        _targetWeaponDis = closestDistance;
                        _targetWeapon = aux;
                    }
                }

            }
            closestDistance = Mathf.Infinity;
            foreach (GameObject aux in healthList)
            {
                if (aux != null)
                {
                    _bot.NavMeshAgent.CalculatePath(aux.transform.position, path);
                    if (GetPathLength(path) < closestDistance)
                    {
                        _targetHealthDis = closestDistance;
                        _targetHealth = aux;
                    }
                }
            }
        }
    }
}

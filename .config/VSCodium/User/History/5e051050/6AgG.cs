using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "VisibleEnemyCon", menuName = "Scriptable Objects/Con/VisibleEnemyCon")]
public class VisibleEnemyCon : ICondition
{

    enum TargetType
    {
        Enemy,
        NPC,
        Weapon,
        Health
    }
    [Range(0f, 180f)]
    [SerializeField] private float maxAngle = 50.0f;

    [SerializeField] private TargetType targetType;
    override public bool CheckCondition(GameObject Entity)
    {
        bool isVisible = false;
        GameObject target = null;
        TargetListControllerCmp targetList = Entity.GetComponent<TargetListControllerCmp>();

        // 1. Asignación del target
        switch (targetType)
        {
            case TargetType.Enemy: target = targetList.GetTargetEnemy(); break;
            case TargetType.NPC: target = targetList.GetTargetNPC(); break;
            case TargetType.Weapon: target = targetList.GetTargetWeapon(); break;
            case TargetType.Health: target = targetList.GetTargetHealth(); break;
        }

        // Si no hay ningún objetivo asignado, no puede ser visible
        if (target == null)
        {
            if (inverse)
            {
                return true;
            }
            return false;
        }

        // 2. Cálculo de dirección y ángulo
        Vector3 origin = Entity.transform.position;
        Vector3 targetPos = target.transform.position;
        Vector3 dirVector = targetPos - origin;
        float ang = Vector3.Angle(Entity.transform.forward, dirVector);

        // 3. Comprobar si está dentro del cono de visión
        if (ang <= maxAngle)
        {
            RaycastHit hit;
            // Lanzamos el Raycast para ver si hay obstáculos
            if (Physics.Raycast(origin, dirVector.normalized, out hit, dirVector.magnitude))
            {
                // Si el primer objeto que golpea es nuestro objetivo, hay línea de visión clara
                if (hit.collider.gameObject == target)
                {
                    isVisible = true;
                }
            }
        }

        // 4. Lógica de inversión
        if (inverse)
        {
            return !isVisible;
        }
        return isVisible;
    }
}

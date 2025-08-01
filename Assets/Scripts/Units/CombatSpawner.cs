using UnityEngine;

public class CombatSpawner : MonoBehaviour
{
    public UnitData unitAData;
    public UnitData unitBData;

    public Transform spawnPointA;
    public Transform spawnPointB;

    void Start()
    {
        // Instanciar unidad A
        GameObject unitA = Instantiate(unitAData.prefab, spawnPointA.position, Quaternion.identity);
        UnitController3D controllerA = unitA.GetComponentInChildren<UnitController3D>();
        controllerA.Initialize(unitAData);

        // Instanciar unidad B
        GameObject unitB = Instantiate(unitBData.prefab, spawnPointB.position, Quaternion.identity);
        UnitController3D controllerB = unitB.GetComponentInChildren<UnitController3D>();
        controllerB.Initialize(unitBData);

        // Asignar objetivos entre sí
        controllerA.enemyTarget = controllerB;
        controllerB.enemyTarget = controllerA;
    }
}

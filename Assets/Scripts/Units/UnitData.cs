using UnityEngine;

public enum UnitType
{
    Melee,
    Ranged,
    Tank,
    Support
}

public enum UnitClass
{
    Swordsman,
    Duelist,
    Assassin,
    Archer,
    Mage,
    Healer,
    Summoner,
    ShieldBearer,
    Golem
}

[CreateAssetMenu(fileName = "NewUnit", menuName = "Units/Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public UnitType unitType;
    public UnitClass unitClass;

    public float maxHealth;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public string descripcion;
    public GameObject prefab;
}

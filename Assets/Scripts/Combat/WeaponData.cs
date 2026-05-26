using UnityEngine;

[CreateAssetMenu(menuName = "ScrambleFarm/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Basic")]
    public WeaponType weaponType;
    public string weaponName;
    [TextArea] public string description;

    [Header("Stat")]
    public StatType referenceStat;

    [Header("Attack")]
    public int baseDamage = 1;
    public float attackRange = 1.5f;
    public float cooldown = 0.5f;
    public float attackSpeedMultiplier = 1f;

    [Header("Prefab")]
    public GameObject attackPrefab;
}
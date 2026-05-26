using UnityEngine;

[CreateAssetMenu(menuName = "ScrambleFarm/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("Basic")]
    public string monsterName;
    public MonsterType monsterType;

    [Header("Stats")]
    public int maxHP = 10;
    public int attackPower = 1;
    public float moveSpeed = 2f;

    [Header("Farm")]
    public int nutrientReward = 40;

    [Header("Score")]
    public int scoreValue = 30;
}
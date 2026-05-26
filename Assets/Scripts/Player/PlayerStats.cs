using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public int HP = 1;
    public int STR = 1;
    public int INT = 1;
    public int VIT = 1;
    public int DEX = 1;
    public int AGI = 1;

    [Header("HP")]
    public int maxHP = 10;
    public int currentHP = 10;

    private void Start()
    {
        RecalculateStats();
        currentHP = maxHP;
    }

    public void RecalculateStats()
    {
        maxHP = 10 + (HP - 1) * 5;
    }

    public float GetMoveSpeed()
    {
        return 5f + AGI * 0.2f;
    }

    public float GetAttackCooldownMultiplier()
    {
        return Mathf.Max(0.5f, 1f - DEX * 0.03f);
    }

    public void TakeDamage(int damage)
    {
        int reducedDamage = Mathf.Max(1, damage - VIT);
        currentHP -= reducedDamage;

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead");
    }

    public void IncreaseStat(CropType cropType)
    {
        switch (cropType)
        {
            case CropType.Turnip:
                HP++;
                break;
            case CropType.Garlic:
                STR++;
                break;
            case CropType.Eggplant:
                INT++;
                break;
            case CropType.Tomato:
                VIT++;
                break;
            case CropType.Carrot:
                DEX++;
                break;
            case CropType.Corn:
                AGI++;
                break;
        }

        RecalculateStats();
    }

    public int GetStatLevelByCrop(CropType cropType)
    {
        switch (cropType)
        {
            case CropType.Turnip:
                return HP;
            case CropType.Garlic:
                return STR;
            case CropType.Eggplant:
                return INT;
            case CropType.Tomato:
                return VIT;
            case CropType.Carrot:
                return DEX;
            case CropType.Corn:
                return AGI;
            default:
                return 1;
        }
    }

    public int GetRequiredCropCountForNextStat(CropType cropType)
    {
        return GetStatLevelByCrop(cropType);
    }
}
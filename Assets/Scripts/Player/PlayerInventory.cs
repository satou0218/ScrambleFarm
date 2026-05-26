using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<CropType, int> cropCounts = new Dictionary<CropType, int>();

    private void Awake()
    {
        foreach (CropType type in System.Enum.GetValues(typeof(CropType)))
        {
            cropCounts[type] = 0;
        }
    }

    public void AddCrop(CropType cropType, int amount)
    {
        cropCounts[cropType] += amount;

        Debug.Log($"{cropType} を {amount} 個入手しました。現在：{cropCounts[cropType]}");
    }

    public bool ConsumeCrop(CropType cropType, int amount)
    {
        if (!cropCounts.ContainsKey(cropType))
        {
            return false;
        }

        if (cropCounts[cropType] < amount)
        {
            Debug.Log($"{cropType} が足りません。必要数：{amount}、所持数：{cropCounts[cropType]}");
            return false;
        }

        cropCounts[cropType] -= amount;

        Debug.Log($"{cropType} を {amount} 個消費しました。残り：{cropCounts[cropType]}");

        return true;
    }

    public int GetCropCount(CropType cropType)
    {
        if (!cropCounts.ContainsKey(cropType))
        {
            return 0;
        }

        return cropCounts[cropType];
    }
}
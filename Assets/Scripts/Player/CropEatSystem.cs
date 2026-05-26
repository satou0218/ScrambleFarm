using UnityEngine;

public class CropEatSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerInventory playerInventory;

    public bool TryEatCrop(CropType cropType)
    {
        if (playerStats == null || playerInventory == null)
        {
            Debug.LogWarning("PlayerStats または PlayerInventory が登録されていません");
            return false;
        }

        int requiredCount = playerStats.GetRequiredCropCountForNextStat(cropType);
        int currentCount = playerInventory.GetCropCount(cropType);

        if (currentCount < requiredCount)
        {
            Debug.Log($"作物が足りません。必要数: {requiredCount}, 所持数: {currentCount}");
            return false;
        }

        bool consumed = playerInventory.ConsumeCrop(cropType, requiredCount);

        if (!consumed)
        {
            return false;
        }

        playerStats.IncreaseStat(cropType);

        Debug.Log($"{cropType} を {requiredCount} 個食べてステータスアップ");
        return true;
    }
}
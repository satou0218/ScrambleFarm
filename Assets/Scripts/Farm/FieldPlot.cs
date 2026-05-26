using UnityEngine;

public class FieldPlot : MonoBehaviour
{
    [Header("Water / Nutrient")]
    [SerializeField] private float water = 100f;
    [SerializeField] private float nutrient = 100f;

    [SerializeField] private float waterDecreasePerSecond = 0.5f;
    [SerializeField] private float nutrientDecreasePerSecond = 0.2f;

    [Header("Crop")]
    [SerializeField] private Crop currentCrop;

    public float Water => water;
    public float Nutrient => nutrient;
    public bool HasCrop => currentCrop != null;

    private void Update()
    {
        DecreaseWaterAndNutrient();
    }

    private void DecreaseWaterAndNutrient()
    {
        water -= waterDecreasePerSecond * Time.deltaTime;
        nutrient -= nutrientDecreasePerSecond * Time.deltaTime;

        water = Mathf.Clamp(water, 0f, 100f);
        nutrient = Mathf.Clamp(nutrient, 0f, 100f);
    }

    public float GetGrowthMultiplier()
    {
        bool hasWater = water > 0f;
        bool hasNutrient = nutrient > 0f;

        if (hasWater && hasNutrient) return 1f;
        if (hasWater || hasNutrient) return 0.5f;
        return 0f;
    }

    public void Watering()
    {
        water = 100f;
        Debug.Log("畑の水分が100まで回復しました");
    }

    public void AddNutrient(float amount)
    {
        nutrient += amount;
        nutrient = Mathf.Clamp(nutrient, 0f, 100f);

        Debug.Log($"畑の栄養が {amount} 回復しました。現在の栄養：{nutrient}");
    }

    public bool PlantCrop(Crop cropPrefab, CropData cropData)
    {
        if (HasCrop)
        {
            Debug.Log("この畑にはすでに作物があります");
            return false;
        }

        if (cropPrefab == null)
        {
            Debug.LogWarning("Crop Prefab が設定されていません");
            return false;
        }

        if (cropData == null)
        {
            Debug.LogWarning("Crop Data が設定されていません");
            return false;
        }

        Vector3 spawnPosition = transform.position + Vector3.up * 0.05f;

        Crop crop = Instantiate(
            cropPrefab,
            spawnPosition,
            Quaternion.identity
        );

        crop.Initialize(cropData, this);

        currentCrop = crop;

        Debug.Log($"{cropData.cropName} を植えました");

        return true;
    }

    public void ClearCrop()
    {
        currentCrop = null;
    }
}
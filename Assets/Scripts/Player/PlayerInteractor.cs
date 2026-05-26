using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SeedSelector seedSelector;
    [SerializeField] private Crop cropPrefab;

    [Header("Settings")]
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask fieldLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TryPlantSeed();
        }
    }

    private void TryPlantSeed()
    {
        FieldPlot nearestField = FindNearestField();

        if (nearestField == null)
        {
            Debug.Log("近くに畑がありません");
            return;
        }

        if (seedSelector == null)
        {
            Debug.LogWarning("SeedSelector が設定されていません");
            return;
        }

        CropData selectedCropData = seedSelector.SelectedCropData;

        if (selectedCropData == null)
        {
            Debug.LogWarning("選択中の種がありません");
            return;
        }

        nearestField.PlantCrop(cropPrefab, selectedCropData);
    }

    private FieldPlot FindNearestField()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            interactRange,
            fieldLayer
        );

        FieldPlot nearestField = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            FieldPlot fieldPlot = hit.GetComponent<FieldPlot>();

            if (fieldPlot == null) continue;

            float distance = Vector3.Distance(
                transform.position,
                fieldPlot.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestField = fieldPlot;
            }
        }

        return nearestField;
    }
}
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Crop : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private int currentStage = 0;
    [SerializeField] private float growthTimer = 0f;

    private CropData cropData;
    private FieldPlot fieldPlot;
    private SpriteRenderer spriteRenderer;

    public CropType CropType => cropData.cropType;
    public bool IsHarvestable => currentStage >= 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(CropData data, FieldPlot plot)
    {
        cropData = data;
        fieldPlot = plot;

        currentStage = 0;
        growthTimer = 0f;

        UpdateSprite();
    }

    private void Update()
    {
        if (cropData == null) return;
        if (IsHarvestable) return;

        growthTimer += Time.deltaTime;

        if (growthTimer >= cropData.growthTimeToStage1)
        {
            GrowToStage1();
        }
    }

    private void GrowToStage1()
    {
        currentStage = 1;
        growthTimer = 0f;

        UpdateSprite();

        Debug.Log($"{cropData.cropName} が収穫可能になりました");
    }

    private void UpdateSprite()
    {
        if (spriteRenderer == null || cropData == null) return;

        if (currentStage == 0)
        {
            spriteRenderer.sprite = cropData.stage0Sprite;
        }
        else
        {
            spriteRenderer.sprite = cropData.stage1Sprite;
        }
    }

    public void Harvest(PlayerInventory inventory)
    {
        if (!IsHarvestable)
        {
            Debug.Log("まだ収穫できません");
            return;
        }

        if (inventory != null)
        {
            inventory.AddCrop(cropData.cropType, 1);
        }

        if (fieldPlot != null)
        {
            fieldPlot.ClearCrop();
        }

        Debug.Log($"{cropData.cropName} を収穫しました");

        Destroy(gameObject);
    }
}
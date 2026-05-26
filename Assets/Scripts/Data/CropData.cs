using UnityEngine;

[CreateAssetMenu(menuName = "ScrambleFarm/Crop Data")]
public class CropData : ScriptableObject
{
    [Header("Basic")]
    public CropType cropType;
    public string cropName;

    [Header("Growth")]
    public float growthTimeToStage1 = 30f;

    [Header("Sprites")]
    public Sprite stage0Sprite;
    public Sprite stage1Sprite;
}
using UnityEngine;

public class SeedSelector : MonoBehaviour
{
    [SerializeField] private CropData[] cropDatas;

    private int selectedIndex = 0;

    public CropData SelectedCropData => cropDatas[selectedIndex];

    private void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0)
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = cropDatas.Length - 1;
            }

            DebugSelectedSeed();
        }
        else if (scroll < 0)
        {
            selectedIndex++;
            if (selectedIndex >= cropDatas.Length)
            {
                selectedIndex = 0;
            }

            DebugSelectedSeed();
        }
    }

    private void DebugSelectedSeed()
    {
        Debug.Log($"選択中の種: {SelectedCropData.cropName}");
    }
}
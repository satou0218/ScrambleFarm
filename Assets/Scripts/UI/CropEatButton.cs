using UnityEngine;
using UnityEngine.UI;

public class CropEatButton : MonoBehaviour
{
    [SerializeField] private CropType cropType;
    [SerializeField] private CropEatSystem cropEatSystem;
    [SerializeField] private GameMenuUI gameMenuUI;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        if (cropEatSystem == null) return;

        bool success = cropEatSystem.TryEatCrop(cropType);

        if (success)
        {
            Debug.Log("作物を食べました");
        }
    }
}
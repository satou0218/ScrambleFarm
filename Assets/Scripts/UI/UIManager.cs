using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Top UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI selectedSeedText;

    [Header("References")]
    [SerializeField] private SeedSelector seedSelector;

    private void Update()
    {
        UpdateTimerUI();
        UpdateSelectedSeedUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;
        if (GameTimer.Instance == null) return;

        timerText.text = GameTimer.Instance.GetTimeText();
    }

    private void UpdateSelectedSeedUI()
    {
        if (selectedSeedText == null) return;
        if (seedSelector == null) return;
        if (seedSelector.SelectedCropData == null) return;

        selectedSeedText.text = $"選択中の種：{seedSelector.SelectedCropData.cropName}";
    }
}
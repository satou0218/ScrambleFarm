using TMPro;
using UnityEngine;

public class GameMenuUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject menuPanel;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI cropInventoryText;
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI controlText;

    [Header("References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private SeedSelector seedSelector;
    [SerializeField] private WeaponManager weaponManager;

    private bool isOpen = false;

    public bool IsOpen => isOpen;

    private void Start()
    {
        CloseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleMenu();
        }

        if (isOpen)
        {
            RefreshMenu();
        }
    }

    private void ToggleMenu()
    {
        if (isOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        isOpen = true;
        menuPanel.SetActive(true);
        RefreshMenu();

    }

    private void CloseMenu()
    {
        isOpen = false;

        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

    }

    private void RefreshMenu()
    {
        UpdateStatusText();
        UpdateCropInventoryText();
        UpdateSkillText();
        UpdateWeaponText();
        UpdateScoreText();
        UpdateControlText();
    }

    private void UpdateStatusText()
    {
        if (statusText == null || playerStats == null) return;

        statusText.text =
            $"【ステータス】\n" +
            $"HP：{playerStats.currentHP} / {playerStats.maxHP}\n" +
            $"STR：{playerStats.STR}\n" +
            $"INT：{playerStats.INT}\n" +
            $"VIT：{playerStats.VIT}\n" +
            $"DEX：{playerStats.DEX}\n" +
            $"AGI：{playerStats.AGI}";
    }

    private void UpdateCropInventoryText()
    {
        if (cropInventoryText == null || playerInventory == null) return;

        cropInventoryText.text =
            $"【所持作物】\n" +
            $"カブ：{playerInventory.GetCropCount(CropType.Turnip)}\n" +
            $"にんにく：{playerInventory.GetCropCount(CropType.Garlic)}\n" +
            $"ナス：{playerInventory.GetCropCount(CropType.Eggplant)}\n" +
            $"トマト：{playerInventory.GetCropCount(CropType.Tomato)}\n" +
            $"ニンジン：{playerInventory.GetCropCount(CropType.Carrot)}\n" +
            $"トウモロコシ：{playerInventory.GetCropCount(CropType.Corn)}";
    }

    private void UpdateSkillText()
    {
        if (skillText == null) return;

        skillText.text =
            $"【所持スキル】\n" +
            $"未実装";
    }

    private void UpdateWeaponText()
    {
        if (weaponText == null) return;

        string weaponName = "なし";

        if (weaponManager != null && weaponManager.CurrentWeapon != null)
        {
            weaponName = weaponManager.CurrentWeapon.weaponName;
        }

        weaponText.text =
            $"【装備中の武器】\n" +
            $"{weaponName}";
    }

    private void UpdateScoreText()
    {
        if (scoreText == null) return;

        scoreText.text =
            $"【スコア】\n" +
            $"0";
    }

    private void UpdateControlText()
    {
        if (controlText == null) return;

        string selectedSeedName = "なし";

        if (seedSelector != null && seedSelector.SelectedCropData != null)
        {
            selectedSeedName = seedSelector.SelectedCropData.cropName;
        }

        controlText.text =
            $"【操作説明】\n" +
            $"WASD：移動\n" +
            $"Space：緊急回避\n" +
            $"X：メニューを閉じる\n" +
            $"F：水を撒く\n" +
            $"左クリック：攻撃 / 収穫\n" +
            $"右クリック：種を植える\n" +
            $"マウススクロール：種変更\n\n" +
            $"選択中の種：{selectedSeedName}";
    }
}
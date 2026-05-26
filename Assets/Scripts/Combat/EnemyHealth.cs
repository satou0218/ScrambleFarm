using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private MonsterData monsterData;

    [Header("HP")]
    [SerializeField] private int currentHP;

    private FieldPlotDetector fieldPlotDetector;

    public void Initialize(MonsterData data)
    {
        monsterData = data;
        currentHP = monsterData.maxHP;
    }

    private void Awake()
    {
        fieldPlotDetector = GetComponent<FieldPlotDetector>();
    }

    private void Start()
    {
        if (monsterData != null)
        {
            currentHP = monsterData.maxHP;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        Debug.Log($"{gameObject.name} に {damage} ダメージ。残りHP：{currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        RecoverFieldNutrient();

        Debug.Log($"{gameObject.name} を倒しました");

        Destroy(gameObject);
    }

    private void RecoverFieldNutrient()
    {
        if (monsterData == null) return;
        if (fieldPlotDetector == null) return;

        FieldPlot fieldPlot = fieldPlotDetector.CurrentFieldPlot;

        if (fieldPlot == null) return;

        fieldPlot.AddNutrient(monsterData.nutrientReward);

        Debug.Log($"畑の栄養が {monsterData.nutrientReward} 回復しました");
    }
}
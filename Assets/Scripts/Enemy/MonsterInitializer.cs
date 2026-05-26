using UnityEngine;

public class MonsterInitializer : MonoBehaviour
{
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private Transform playerTarget;

    private void Start()
    {
        Initialize(monsterData, playerTarget);
    }

    public void Initialize(MonsterData data, Transform target)
    {
        monsterData = data;
        playerTarget = target;

        EnemyHealth health = GetComponent<EnemyHealth>();
        MonsterMove move = GetComponent<MonsterMove>();
        MonsterAttack attack = GetComponent<MonsterAttack>();

        if (health != null)
        {
            health.Initialize(monsterData);
        }

        if (move != null)
        {
            move.Initialize(monsterData, playerTarget);
        }

        if (attack != null)
        {
            attack.Initialize(monsterData);
        }
    }
}
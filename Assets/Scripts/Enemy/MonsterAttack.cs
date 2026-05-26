using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonsterData monsterData;

    [Header("Settings")]
    [SerializeField] private float attackInterval = 1f;

    private float lastAttackTime = -999f;

    public void Initialize(MonsterData data)
    {
        monsterData = data;
    }

    private void OnCollisionStay(Collision collision)
    {
        TryAttackPlayer(collision.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        TryAttackPlayer(other.gameObject);
    }

    private void TryAttackPlayer(GameObject target)
    {
        if (monsterData == null) return;

        PlayerStats playerStats = target.GetComponent<PlayerStats>();

        if (playerStats == null) return;

        if (Time.time < lastAttackTime + attackInterval)
        {
            return;
        }

        lastAttackTime = Time.time;

        playerStats.TakeDamage(monsterData.attackPower);

        Debug.Log($"プレイヤーに {monsterData.attackPower} ダメージ");
    }
}

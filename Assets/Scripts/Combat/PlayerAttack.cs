using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private WeaponManager weaponManager;

    [Header("Attack Position")]
    [SerializeField] private Transform attackPoint;

    [Header("Settings")]
    [SerializeField] private float attackPrefabLifeTime = 0.2f;

    private float lastAttackTime = -999f;
    private Vector3 lastAttackDirection = Vector3.forward;

    private void Update()
    {
        UpdateAttackDirection();

        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    private void UpdateAttackDirection()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, 0f, v);

        if (input.sqrMagnitude > 0.01f)
        {
            lastAttackDirection = input.normalized;
        }
    }

    private void TryAttack()
    {
        if (playerStats == null || weaponManager == null) return;

        WeaponData weapon = weaponManager.CurrentWeapon;

        if (weapon == null)
        {
            Debug.LogWarning("装備中の武器がありません");
            return;
        }

        if (weapon.attackPrefab == null)
        {
            Debug.LogWarning($"{weapon.weaponName} に attackPrefab が設定されていません");
            return;
        }

        float cooldown = CalculateCooldown(weapon);

        if (Time.time < lastAttackTime + cooldown)
        {
            return;
        }

        lastAttackTime = Time.time;

        int damage = CalculateDamage(weapon);

        SpawnAttackPrefab(weapon, damage);
    }

    private int CalculateDamage(WeaponData weapon)
    {
        int statValue = 1;

        switch (weapon.referenceStat)
        {
            case StatType.STR:
                statValue = playerStats.STR;
                break;

            case StatType.INT:
                statValue = playerStats.INT;
                break;
        }

        int finalDamage = weapon.baseDamage + statValue;

        return finalDamage;
    }

    private float CalculateCooldown(WeaponData weapon)
    {
        float dexMultiplier = playerStats.GetAttackCooldownMultiplier();

        float finalCooldown = weapon.cooldown * dexMultiplier / weapon.attackSpeedMultiplier;

        return Mathf.Max(0.05f, finalCooldown);
    }

    private void SpawnAttackPrefab(WeaponData weapon, int damage)
    {
        Vector3 spawnPosition;

        if (attackPoint != null)
        {
            spawnPosition = attackPoint.position;
        }
        else
        {
            spawnPosition = transform.position + lastAttackDirection * weapon.attackRange;
        }

        Quaternion rotation = Quaternion.LookRotation(lastAttackDirection, Vector3.up);

        GameObject attackObject = Instantiate(
            weapon.attackPrefab,
            spawnPosition,
            rotation
        );

        attackObject.transform.localScale = new Vector3(
            weapon.attackRange,
            attackObject.transform.localScale.y,
            weapon.attackRange
        );

        AttackHitbox hitbox = attackObject.GetComponent<AttackHitbox>();

        if (hitbox != null)
        {
            hitbox.Initialize(damage, attackPrefabLifeTime);
        }

        Debug.Log($"{weapon.weaponName} で攻撃。ダメージ：{damage}");
    }
}
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private int damage;
    private float lifeTime = 0.2f;

    private bool initialized = false;

    public void Initialize(int damageValue, float destroyTime)
    {
        damage = damageValue;
        lifeTime = destroyTime;
        initialized = true;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!initialized) return;

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
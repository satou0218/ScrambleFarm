using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private Transform playerTarget;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnRadius = 10f;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        if (monsterPrefab == null) return;
        if (monsterData == null) return;
        if (playerTarget == null) return;

        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 spawnPosition = playerTarget.position + new Vector3(
            randomCircle.x,
            0f,
            randomCircle.y
        );

        GameObject monster = Instantiate(
            monsterPrefab,
            spawnPosition,
            Quaternion.identity
        );

        MonsterInitializer initializer = monster.GetComponent<MonsterInitializer>();

        if (initializer != null)
        {
            initializer.Initialize(monsterData, playerTarget);
        }
    }
}
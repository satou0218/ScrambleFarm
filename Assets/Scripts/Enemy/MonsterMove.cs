using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MonsterMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private Transform target;

    private Rigidbody rb;

    public void Initialize(MonsterData data, Transform playerTarget)
    {
        monsterData = data;
        target = playerTarget;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (monsterData == null) return;
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f) return;

        direction.Normalize();

        Vector3 nextPosition = rb.position + direction * monsterData.moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
    }
}
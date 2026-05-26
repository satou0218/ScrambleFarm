using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Move Limit")]
    [SerializeField] private float minX = -25f;
    [SerializeField] private float maxX = 25f;
    [SerializeField] private float minZ = -25f;
    [SerializeField] private float maxZ = 25f;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 lastMoveDirection = Vector3.forward;

    public Vector3 MoveInput => moveInput;
    public Vector3 LastMoveDirection => lastMoveDirection;
    public bool IsMoving => moveInput.sqrMagnitude > 0.01f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(h, 0f, v).normalized;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput;
        }

        Debug.Log($"MoveInput: {moveInput}");
    }

    private void FixedUpdate()
{
    if (moveInput.sqrMagnitude <= 0.01f)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        return;
    }

    Vector3 nextPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;

    nextPosition.x = Mathf.Clamp(nextPosition.x, minX, maxX);
    nextPosition.z = Mathf.Clamp(nextPosition.z, minZ, maxZ);

    rb.MovePosition(nextPosition);
}
}
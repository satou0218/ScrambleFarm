using UnityEngine;

public class PlayerHarvest : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float harvestRange = 2f;
    [SerializeField] private LayerMask cropLayer;

    private PlayerInventory inventory;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryHarvest();
        }
    }

    private void TryHarvest()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            harvestRange,
            cropLayer
        );

        foreach (Collider hit in hits)
        {
            Crop crop = hit.GetComponent<Crop>();

            if (crop == null) continue;

            crop.Harvest(inventory);
            return;
        }

        Debug.Log("収穫できる作物が近くにありません");
    }
}
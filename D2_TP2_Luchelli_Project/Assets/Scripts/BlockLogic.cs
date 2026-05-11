using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Data_Block blockData;

    [Header("Cached Components")]
    [SerializeField] private Rigidbody rb;

    private bool hasLanded = false;

    private void Awake()
    {
        ApplyData();
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded)
        {
            if (collision.gameObject.CompareTag("Base"))
            {
                hasLanded = true;

                EvaluatePlacement(collision);
            }
        }
    }

    /// <summary>
    /// Applies ScriptableObject configuration
    /// </summary>
    private void ApplyData()
    {
        if (blockData == null)
        {
            Debug.LogWarning("[Block] Missing BlockData");
            return;
        }

        rb.mass = blockData.mass;
        rb.linearDamping = blockData.drag;
        rb.angularDamping = blockData.angularDrag;

        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && blockData.blockMaterial != null)
        {
            renderer.material = blockData.blockMaterial;
        }
    }

    /// <summary>
    /// Evaluates how well the block was placed
    /// </summary>
    private void EvaluatePlacement(Collision collision)
    {
        Transform other = collision.transform;

        float offset = Mathf.Abs(transform.position.x - other.position.x);

        Debug.Log($"[Block] Offset: {offset}");

        if (offset < blockData.perfectOffsetThreshold)
        {
            TowerManager.Instance.RegisterPlacement(true);
            Debug.Log("[Block] Perfect placement!");
        }
        else if (offset < blockData.goodOffsetThreshold)
        {
            TowerManager.Instance.RegisterPlacement(false);
            Debug.Log("[Block] Good placement");
        }
        else
        {
            Debug.Log("[Block] Miss!");
            Die();
            return;
        }

        PlaceBlock(collision);
    }

    /// <summary>
    /// Placement logic
    /// </summary>
    private void PlaceBlock(Collision collision)
    {
        //Set Block tag to Base's tag for next collisions
        gameObject.tag = collision.gameObject.tag;
        // Freeze Block's rotation and movement and set all constriants to true
        rb.freezeRotation = true;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// Destroy logic when misplaced
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

}

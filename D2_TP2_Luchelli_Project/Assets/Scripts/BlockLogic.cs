using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Data_Block blockData;

    [Header("Cached Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float killZonePosOffset = -5f;

    private BoxCollider blockCollider;

    private bool hasLanded = false;

    private void Awake()
    {
        ApplyData();
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if(blockCollider == null)
            blockCollider = GetComponent<BoxCollider>();
        if (audioSource == null) 
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!hasLanded && transform.position.y < killZonePosOffset)
        {
            hasLanded = true; 
            TowerManager.Instance.RegisterMiss();
            Die();
        }
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
            PlaySound(blockData.perfectImpactSound);
            Debug.Log("[Block] Perfect placement!");
        }
        else if (offset < blockData.goodOffsetThreshold)
        {
            TowerManager.Instance.RegisterPlacement(false);
            PlaySound(blockData.impactSound);
            Debug.Log("[Block] Good placement");
        }
        else
        {
            TowerManager.Instance.RegisterMiss();
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
    /// Plays a specific audio clip through the block's AudioSource
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (blockData != null && clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Destroy logic when misplaced
    /// </summary>
    private void Die()
    {
        Debug.Log("[Block] Miss!");
        PlaySound(blockData.explodeSound);
        blockCollider.enabled = false;
        Destroy(gameObject, 0.5f);
    }

}

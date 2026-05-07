using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    private bool hasLanded = false;

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
    /// Evaluates how well the block was placed
    /// </summary>
    private void EvaluatePlacement(Collision collision)
    {
        Transform other = collision.transform;

        float offset = Mathf.Abs(transform.position.x - other.position.x);

        Debug.Log($"Offset: {offset}");

        if (offset < 0.2f)
        {
            TowerManager.Instance.RegisterPlacement(true);
            Debug.Log("Perfect placement!");
        }
        else if (offset < 0.5f)
        {
            TowerManager.Instance.RegisterPlacement(false);
            Debug.Log("Good placement");
        }
        else
        {
            Debug.Log("Miss!");
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
        rb = GetComponent<Rigidbody>();
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

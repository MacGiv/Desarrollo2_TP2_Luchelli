using UnityEngine;

/// <summary>
/// Handles automatic horizontal movement and block spawning
/// </summary>
public class BlockSpawner : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Horizontal movement speed")]
    [SerializeField] private float moveSpeed = 2f;

    [Tooltip("Movement range on X axis")]
    [SerializeField] private float moveRange = 5f;

    [Header("Spawning")]
    [Tooltip("Block prefab to spawn")]
    [SerializeField] private GameObject blockPrefab;

    [Tooltip("Spawn point for blocks")]
    [SerializeField] private Transform spawnPoint;

    private Vector3 startPosition;
    private float direction = 1f;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        HandleMovement();
        HandleInput();
    }

    /// <summary>
    /// Moves the spawner left and right
    /// </summary>
    private void HandleMovement()
    {
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        if (transform.position.x > startPosition.x + moveRange)
            direction = -1f;

        if (transform.position.x < startPosition.x - moveRange)
            direction = 1f;
    }

    /// <summary>
    /// Handles player input to spawn blocks
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBlock();
        }
    }

    /// <summary>
    /// Instantiates a block at spawn point
    /// </summary>
    private void SpawnBlock()
    {
        if (blockPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Spawner is missing references");
            return;
        }

        Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);
    }
}
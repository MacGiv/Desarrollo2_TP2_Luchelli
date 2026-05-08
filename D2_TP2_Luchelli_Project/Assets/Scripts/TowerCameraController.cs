using UnityEngine;

/// <summary>
/// Handles vertical camera tracking
/// </summary>
public class TowerCameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private float heightPerBlock = 1f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float yOffset = 0f;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void OnEnable()
    {
        TowerManager.OnHeightChanged += HandleHeightChanged;
    }

    private void OnDisable()
    {
        TowerManager.OnHeightChanged -= HandleHeightChanged;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Updates target camera height
    /// </summary>
    private void HandleHeightChanged(int height)
    {
        float targetY = (height * heightPerBlock) + yOffset;

        targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}
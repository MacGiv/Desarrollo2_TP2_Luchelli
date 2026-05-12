using UnityEngine;

/// <summary>
/// Handles smooth vertical camera tracking
/// </summary>
public class TowerCameraController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Shared tower settings")]
    [SerializeField] private Data_Tower towerSettings;

    [Header("Camera Settings")]
    [Tooltip("Camera smoothing speed")]
    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 targetPosition;

    private float initialY;

    private void Start()
    {
        initialY = transform.position.y;

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

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Updates camera target position based on tower height
    /// </summary>
    private void HandleHeightChanged(int height)
    {
        if (towerSettings == null)
        {
            Debug.LogWarning("[TowerCameraController] Missing TowerSettings reference");
            return;
        }

        float targetY = initialY + (height * towerSettings.heightPerBlock);

        targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}
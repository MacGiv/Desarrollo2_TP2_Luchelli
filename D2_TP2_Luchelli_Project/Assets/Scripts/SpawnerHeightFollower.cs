using UnityEngine;

/// <summary>
/// Updates spawner height based on tower progression
/// </summary>
public class SpawnerHeightFollower : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Shared tower settings")]
    [SerializeField] private Data_Tower towerSettings;

    private float initialY;

    private void Start()
    {
        initialY = transform.position.y;
    }

    private void OnEnable()
    {
        TowerManager.OnHeightChanged += HandleHeightChanged;
    }

    private void OnDisable()
    {
        TowerManager.OnHeightChanged -= HandleHeightChanged;
    }

    /// <summary>
    /// Updates spawner vertical position
    /// </summary>
    private void HandleHeightChanged(int height)
    {
        if (towerSettings == null)
        {
            Debug.LogWarning("Missing TowerSettings reference");
            return;
        }

        float targetY = initialY + (height * towerSettings.heightPerBlock);

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}
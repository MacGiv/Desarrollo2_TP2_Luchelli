using System;
using UnityEngine;

/// <summary>
/// Handles tower progression and score
/// </summary>
public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    /// <summary>
    /// Triggered when score changes
    /// </summary>
    public static event Action<int> OnScoreChanged;

    /// <summary>
    /// Triggered when height changes
    /// </summary>
    public static event Action<int> OnHeightChanged;

    /// <summary>
    /// Triggered when strikes change
    /// </summary>
    public static event Action<int> OnStrikeChanged;

    [Header("Score")]
    [SerializeField] private int currentScore;

    [SerializeField] private int currentHeight;

    [SerializeField] private int currentStrikes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Registers a successful placement
    /// </summary>
    public void RegisterPlacement(bool perfectPlacement)
    {
        currentHeight++;

        if (perfectPlacement)
        {
            currentStrikes++;
            currentScore += 2;
        }
        else
        {
            currentStrikes = 0;
            currentScore += 1;
        }

        OnScoreChanged?.Invoke(currentScore);
        OnHeightChanged?.Invoke(currentHeight);
        OnStrikeChanged?.Invoke(currentStrikes);

        Debug.Log($"Score: {currentScore}");
    }
}
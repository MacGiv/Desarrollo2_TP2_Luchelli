using System;
using UnityEngine;

/// <summary>
/// Handles tower progression, score, and high score
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
    /// Triggered when the perfect placement streak changes
    /// </summary>
    public static event Action<int> OnStreakChanged;

    /// <summary>
    /// Triggered when a new high score is reached
    /// </summary>
    public static event Action<int> OnHighScoreChanged;

    /// <summary>
    /// Triggered when ends placement (normal/perfect/miss)
    /// </summary>
    public static event Action OnPlacementResolved;

    /// <summary>
    /// Triggered when you lose
    /// </summary>
    public static event Action OnGameOver;

    [Header("Current Stats")]
    [Tooltip("Current gameplay score")]
    [SerializeField] private int currentScore;

    [Tooltip("Current tower height (blocks placed)")]
    [SerializeField] private int currentHeight;

    [Tooltip("Consecutive perfect placements")]
    [SerializeField] private int currentStreak;

    [Header("Persistence")]
    [Tooltip("Highest score achieved across sessions")]
    [SerializeField] private int highScore;

    private const string HIGH_SCORE_PREF = "HighScorePref";

    private void Awake()
    {
        // Scene-local singleton. We DO NOT use DontDestroyOnLoad here
        // because we want the score to reset when reloading the Gameplay scene. :D
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Load High Score at start
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_PREF, 0);
    }

    /// <summary>
    /// Registers a successful placement
    /// </summary>
    public void RegisterPlacement(bool perfectPlacement)
    {
        currentHeight++;

        if (perfectPlacement)
        {
            currentStreak++;
            currentScore += 2;
        }
        else
        {
            currentStreak = 0;
            currentScore += 1;
        }

        // High Score evaluation
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_PREF, highScore);
            PlayerPrefs.Save();

            OnHighScoreChanged?.Invoke(highScore);
        }

        OnScoreChanged?.Invoke(currentScore);
        OnHeightChanged?.Invoke(currentHeight);
        OnStreakChanged?.Invoke(currentStreak);
        OnPlacementResolved?.Invoke();

        Debug.Log($"[TowerManager] Score: {currentScore} | Streak: {currentStreak} | HighScore: {highScore}");
    }

    /// <summary>
    /// Registers when block missed the tower
    /// </summary>
    public void RegisterMiss()
    {
        Debug.Log("[TowerManager] ¡Missed! Game Over.");
        OnGameOver?.Invoke();
    }

    /// <summary>
    /// Returns the saved high score. 
    /// </summary>
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_PREF, 0);
    }

    /// <summary>
    /// Returns the current score
    /// </summary>
    public int GetCurrentScore() => currentScore;
}
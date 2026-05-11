using TMPro;
using UnityEngine;

/// <summary>
/// Handles gameplay UI updates via Observer pattern
/// </summary>
public class GameplayHUD : MonoBehaviour
{
    [Header("HUD References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI streakText;

    private void OnEnable()
    {
        TowerManager.OnScoreChanged += UpdateScore;
        TowerManager.OnHeightChanged += UpdateHeight;
        TowerManager.OnStreakChanged += UpdateStreak;
    }

    private void OnDisable()
    {
        TowerManager.OnScoreChanged -= UpdateScore;
        TowerManager.OnHeightChanged -= UpdateHeight;
        TowerManager.OnStreakChanged -= UpdateStreak;
    }

    /// <summary>
    /// Updates score UI
    /// </summary>
    private void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    /// <summary>
    /// Updates height UI
    /// </summary>
    private void UpdateHeight(int height)
    {
        heightText.text = $"Height: {height}";
    }

    /// <summary>
    /// Updates streak UI
    /// </summary>
    private void UpdateStreak(int streak)
    {
        streakText.text = $"Streak: {streak}";
    }
}
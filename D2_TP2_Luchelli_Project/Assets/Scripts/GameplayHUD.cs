using TMPro;
using UnityEngine;

public class GameplayHUD : MonoBehaviour
{
    [Header("HUD References")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI heightText;

    [SerializeField] private TextMeshProUGUI strikeText;

    private void OnEnable()
    {
        TowerManager.OnScoreChanged += UpdateScore;
        TowerManager.OnHeightChanged += UpdateHeight;
        TowerManager.OnStrikeChanged += UpdateStrikes;
    }

    private void OnDisable()
    {
        TowerManager.OnScoreChanged -= UpdateScore;
        TowerManager.OnHeightChanged -= UpdateHeight;
        TowerManager.OnStrikeChanged -= UpdateStrikes;
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
    /// Updates strikes UI
    /// </summary>
    private void UpdateStrikes(int strikes)
    {
        strikeText.text = $"Strikes: {strikes}";
    }
}

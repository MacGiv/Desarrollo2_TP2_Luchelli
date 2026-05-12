using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Handles the Game Over UI panel logic and navigation.
/// </summary>
public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestartClicked);
        menuButton.onClick.AddListener(OnMenuClicked);
    }

    private void OnEnable()
    {
        TowerManager.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        TowerManager.OnGameOver -= ShowGameOver;
    }

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Displays the game over panel, updates the final score, and freezes the game.
    /// </summary>
    private void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (finalScoreText != null && TowerManager.Instance != null)
        {
            finalScoreText.text = $"FINAL SCORE: {TowerManager.Instance.GetCurrentScore()}";
        }

        Time.timeScale = 0f;
    }

    /// <summary>
    /// Reloads the current gameplay scene when the restart button is clicked.
    /// </summary>
    private void OnRestartClicked()
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadGameplay();
    }

    /// <summary>
    /// Loads the main menu scene when the menu button is clicked.
    /// </summary>
    private void OnMenuClicked()
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadMainMenu();
    }
}
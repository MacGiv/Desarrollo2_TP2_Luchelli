using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controls the Main Menu navigation and high score display.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Display")]
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        // Setup initial state
        ShowMainPanel();

        // Link persistent buttons
        playButton.onClick.AddListener(OnPlayClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        creditsButton.onClick.AddListener(OnCreditsClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    private void Start()
    {
        UpdateHighScoreDisplay();
        AudioManager.Instance.PlayMenuMusic();
    }

    /// <summary>
    /// Updates the UI with the saved high score from PlayerPrefs.
    /// </summary>
    private void UpdateHighScoreDisplay()
    {
        int high = PlayerPrefs.GetInt("HighScorePref", 0);
        if (highScoreText != null)
        {
            highScoreText.text = $"BEST SCORE: {high}";
        }
    }

    /// <summary>
    /// Navigation Logic 
    /// </summary>
    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private void OnPlayClicked()
    {
        SceneLoader.Instance.LoadGameplay();
    }

    private void OnSettingsClicked()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    private void OnCreditsClicked()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    private void OnExitClicked()
    {
        SceneLoader.Instance.ExitGame();
    }
}
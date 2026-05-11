using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Listens to pause events to show/hide the pause menu and handles button actions.
/// </summary>
public class PauseMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [Tooltip("Main container for the pause menu UI")]
    [SerializeField] private GameObject pausePanel;
    [Tooltip("Settings panel to toggle over the pause menu")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    private PauseManager pauseManager;

    private void Awake()
    {
        // Start hidden
        pausePanel.SetActive(false);
        if (settingsPanel != null) 
            settingsPanel.SetActive(false);

        // Find the local PauseManager in the scene
        pauseManager = FindFirstObjectByType<PauseManager>();
    }

    private void OnEnable()
    {
        PauseManager.OnPauseStateChanged += HandlePauseState;

        resumeButton.onClick.AddListener(OnResumeClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    private void OnDisable()
    {
        PauseManager.OnPauseStateChanged -= HandlePauseState;

        resumeButton.onClick.RemoveListener(OnResumeClicked);
        settingsButton.onClick.RemoveListener(OnSettingsClicked);
        mainMenuButton.onClick.RemoveListener(OnMainMenuClicked);
    }

    /// <summary>
    /// Shows or hides the pause panel based on the game's pause state
    /// </summary>
    private void HandlePauseState(bool isPaused)
    {
        pausePanel.SetActive(isPaused);

        // Ensure settings panel is closed for next time
        if (!isPaused && settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    private void OnResumeClicked()
    {
        if (pauseManager != null)
        {
            pauseManager.ResumeGame();
        }
    }

    private void OnSettingsClicked()
    {
        if (settingsPanel != null)
        {
            pausePanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
    }

    private void OnMainMenuClicked()
    {
        SceneLoader.Instance.LoadMainMenu();
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

}
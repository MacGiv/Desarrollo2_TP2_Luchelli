using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene transitions globally
/// </summary>
public class SceneLoader : MonoBehaviourSingleton<SceneLoader>
{
    
    /// <summary>
    /// Loads gameplay scene
    /// </summary>
    public void LoadGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    /// <summary>
    /// Loads main menu scene
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Exits application
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("[SceneLoader] Exit Game");

        Application.Quit();
    }
}
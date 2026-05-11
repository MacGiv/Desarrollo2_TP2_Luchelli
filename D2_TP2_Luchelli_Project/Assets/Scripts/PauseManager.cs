using System;
using UnityEngine;

/// <summary>
/// Handles the game's pause state and time manipulation.
/// </summary>
public class PauseManager : MonoBehaviour
{
    /// <summary>
    /// Triggered when the game pauses (true) or resumes (false).
    /// </summary>
    public static event Action<bool> OnPauseStateChanged;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Toggles the current pause state.
    /// </summary>
    public void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;

        OnPauseStateChanged?.Invoke(isPaused);

        Debug.Log($"[PauseManager] Game Paused: {isPaused}");
    }

    /// <summary>
    /// Force resume game (for UI buttons)
    /// </summary>
    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }

    private void OnDestroy()
    {
        // SAFE-GUARD: ensure time is normal if this scene is destroyed while paused
        Time.timeScale = 1f;
    }
}
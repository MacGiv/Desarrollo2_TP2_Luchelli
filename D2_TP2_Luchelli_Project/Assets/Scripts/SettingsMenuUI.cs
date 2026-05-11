using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles audio settings UI
/// </summary>
public class SettingsMenuUI : MonoBehaviour
{
    [Header("Audio Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    private void OnEnable()
    {
        LoadSliderValues();
        RegisterListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    /// <summary>
    /// Loads saved values into sliders
    /// </summary>
    private void LoadSliderValues()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolumePref", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumePref", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumePref", 1f);
        uiSlider.value = PlayerPrefs.GetFloat("UIVolumePref", 1f);
    }

    /// <summary>
    /// Registers slider listeners
    /// </summary>
    private void RegisterListeners()
    {
        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        uiSlider.onValueChanged.AddListener(AudioManager.Instance.SetUIVolume);
    }

    private void RemoveListeners()
    {
        masterSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetSFXVolume);
        uiSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetUIVolume);
    }
}
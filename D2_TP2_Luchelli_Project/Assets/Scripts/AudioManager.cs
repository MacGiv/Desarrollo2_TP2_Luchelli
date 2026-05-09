using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [Tooltip("Main audio mixer")]
    [SerializeField] private AudioMixer audioMixer;

    private const string MASTER_VOLUME = "MasterVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SFX_VOLUME = "SFXVolume";
    private const string UI_VOLUME = "UIVolume";

    private const string MASTER_PREF = "MasterVolumePref";
    private const string MUSIC_PREF = "MusicVolumePref";
    private const string SFX_PREF = "SFXVolumePref";
    private const string UI_PREF = "UIVolumePref";

    private void Awake()
    {
        SetupSingleton();

        LoadVolumes();
    }

    /// <summary>
    /// Initializes singleton instance
    /// </summary>
    private void SetupSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Sets master volume
    /// </summary>
    public void SetMasterVolume(float value)
    {
        SetVolume(MASTER_VOLUME, MASTER_PREF, value);
    }

    /// <summary>
    /// Sets music volume
    /// </summary>
    public void SetMusicVolume(float value)
    {
        SetVolume(MUSIC_VOLUME, MUSIC_PREF, value);
    }

    /// <summary>
    /// Sets SFX volume
    /// </summary>
    public void SetSFXVolume(float value)
    {
        SetVolume(SFX_VOLUME, SFX_PREF, value);
    }

    /// <summary>
    /// Sets UI volume
    /// </summary>
    public void SetUIVolume(float value)
    {
        SetVolume(UI_VOLUME, UI_PREF, value);
    }

    /// <summary>
    /// Applies mixer value and saves preference
    /// </summary>
    private void SetVolume(string parameter, string prefKey, float value)
    {
        // Mathf.Log10(value) * 20f used to "normalize" decibel values to a slider range (0 to 1)
        audioMixer.SetFloat(parameter, Mathf.Log10(value) * 20f);

        PlayerPrefs.SetFloat(prefKey, value);

        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads saved volume settings
    /// </summary>
    private void LoadVolumes()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(MASTER_PREF, 1f));
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_PREF, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_PREF, 1f));
        SetUIVolume(PlayerPrefs.GetFloat(UI_PREF, 1f));
    }
}
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Manages audio mixer volumes and persistent background music across scenes.
/// </summary>
public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [Header("Mixer")]
    [Tooltip("Main audio mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Background Music")]
    [Tooltip("Audio source dedicated to playing background music")]
    [SerializeField] private AudioSource bgmSource;
    [Tooltip("Background music clip for the main menu")]
    [SerializeField] private AudioClip menuMusic;
    [Tooltip("Background music clip for the gameplay loop")]
    [SerializeField] private AudioClip gameplayMusic;

    private const string MASTER_VOLUME = "MasterVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SFX_VOLUME = "SFXVolume";
    private const string UI_VOLUME = "UIVolume";

    private const string MASTER_PREF = "MasterVolumePref";
    private const string MUSIC_PREF = "MusicVolumePref";
    private const string SFX_PREF = "SFXVolumePref";
    private const string UI_PREF = "UIVolumePref";

    protected override void Awake()
    {
        base.Awake();

        // Auto-assign AudioSource if we forgot to drag it in the inspector
        if (bgmSource == null)
        {
            bgmSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        LoadVolumes();
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
        // Mathf.Log10(value) * 20f used to "normalize" decibel values to a slider range (0 to 1) but sliders shouldn't
        // get to 0 value because [Mathf.Log10(0) = -Infinity] and that leads to an AudioMixer's console error
        float clampedValue = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat(parameter, Mathf.Log10(clampedValue) * 20f);
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

    /// <summary>
    /// Plays the main menu background music.
    /// </summary>
    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    /// <summary>
    /// Plays the gameplay background music.
    /// </summary>
    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    /// <summary>
    /// Handles switching the background music seamlessly.
    /// </summary>
    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || bgmSource == null) return;

        // Prevent restarting the track if the exact same song is already playing
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }
}
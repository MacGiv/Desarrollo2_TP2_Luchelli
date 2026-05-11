using UnityEngine;

/// <summary>
/// Generic singleton base class
/// </summary>
public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    /// <summary>
    /// Initializes singleton instance
    /// </summary>
    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;

        DontDestroyOnLoad(gameObject);
    }
}
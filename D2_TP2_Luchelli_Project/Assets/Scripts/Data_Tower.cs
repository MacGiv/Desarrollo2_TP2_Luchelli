using UnityEngine;

/// <summary>
/// Shared tower progression settings
/// </summary>
[CreateAssetMenu(fileName = "Data_Tower", menuName = "Data/TowerSettings")]
public class Data_Tower : ScriptableObject
{
    [Header("Tower Progression")]
    [Tooltip("Vertical height added per placed block")]
    public float heightPerBlock = 1f;
}
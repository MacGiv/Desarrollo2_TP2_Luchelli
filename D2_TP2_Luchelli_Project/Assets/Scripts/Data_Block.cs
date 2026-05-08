using UnityEngine;

[CreateAssetMenu(fileName = "Data_Block", menuName = "Data/BlockData")]

public class Data_Block : ScriptableObject
{
    [Header("Physics")]
    public float mass = 1f;

    public float drag = 0f;

    public float angularDrag = 0.05f;

    [Header("Placement")]
    public float perfectOffsetThreshold = 0.2f;

    public float goodOffsetThreshold = 0.5f;

    [Header("Visual")]
    public Material blockMaterial;
}

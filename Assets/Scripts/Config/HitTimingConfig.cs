using UnityEngine;

[CreateAssetMenu(fileName = "HitTimingConfig", menuName = "Rhythm/Hit Timing Config")]
public class HitTimingConfig : ScriptableObject
{
    [Header("Timing Thresholds (in seconds)")]
    public float perfectThreshold = 0.07f;
    public float greatThreshold = 0.15f;
    public float goodThreshold = 0.3f;
}

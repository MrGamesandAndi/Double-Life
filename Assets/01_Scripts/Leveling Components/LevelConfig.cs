using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName ="Level System/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public AnimationCurve animationCurve;
    public int maxLevel;
    public int maxRequiredExp;

    public int GetRequiredExp(int level)
    {
        int requiredExp = Mathf.RoundToInt(animationCurve.Evaluate(Mathf.InverseLerp(0f, maxLevel, level)) * maxRequiredExp);
        return requiredExp;
    }
}

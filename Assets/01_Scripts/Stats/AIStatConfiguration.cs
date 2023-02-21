using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class AIStatConfiguration 
    {
        [field: SerializeField] public AIStat LinkedStat { get; private set; }
        [field: SerializeField] public bool OverrideDefaults { get; private set; } = false;
        [field: SerializeField, Range(0f, 1f)] public float OverrideInitialValue { get; protected set; } = 0.5f;
        [field: SerializeField, Range(0f, 1f)] public float OverrideDecayRate { get; protected set; } = 0.005f;


    }
}
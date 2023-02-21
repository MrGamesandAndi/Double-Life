using Localisation;
using MemorySystem;
using System;
using UnityEngine;

namespace SmartInteractions
{
    [Serializable]
    public class InteractionOutcome
    {
        public LocalisedString description;
        [Range(0f, 1f)] 
        public float weighting = 1f;
        public float statMultiplier = 1f;
        public bool abandonInteraction = false;
        public InteractionStatChange[] statChanges;
        public MemoryFragment[] memoriesCaused;
        public float NormalisedWeighting { get; set; } = -1f;

    }
}

using System;
using UnityEngine;

namespace TraitSystem
{
    [Serializable]
    public class TraitElement
    {
        [Header("Scoring Scales")]
        [Range(0.5f, 1.5f)] public float _scoringPositiveScale = 1f;
        [Range(0.5f, 1.5f)] public float _scoringNegativeScale = 1f;

        [Header("Impact Scales")]
        [Range(0.5f, 1.5f)] public float _impactPositiveScale = 1f;
        [Range(0.5f, 1.5f)] public float _impactNegativeScale = 1f;

        [Header("Decay Rate")]
        [Range(0.5f, 1.5f)] public float _decayRateScale = 1f;
    }
}

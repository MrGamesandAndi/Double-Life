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
    }
}

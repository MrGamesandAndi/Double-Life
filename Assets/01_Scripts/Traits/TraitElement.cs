using Stats;
using System;
using UnityEngine;

namespace TraitSystem
{
    [Serializable]
    public class TraitElement
    {
        public AIStat _linkedStat;

        [Header("Scoring Scales")]
        [Range(0.5f, 1.5f)] public float _scoringPositiveScale = 1f;
        [Range(0.5f, 1.5f)] public float _scoringNegativeScale = 1f;

        [Header("Impact Scales")]
        [Range(0.5f, 1.5f)] public float _impactPositiveScale = 1f;
        [Range(0.5f, 1.5f)] public float _impactNegativeScale = 1f;

        [Header("Decay Rate")]
        [Range(0.5f, 1.5f)] public float _decayRateScale = 1f;

        public float Apply(AIStat targetStat, TargetType targetType, float currentValue)
        {
            if (targetStat == _linkedStat)
            {
                if (targetType == TargetType.Decay_Rate)
                {
                    currentValue *= _decayRateScale;
                }
                else if (targetType == TargetType.Impact)
                {
                    if (currentValue > 0)
                    {
                        currentValue *= _impactPositiveScale;
                    }
                    else if (currentValue < 0)
                    {
                        currentValue *= _impactNegativeScale;
                    }
                }
                else
                {
                    if (currentValue > 0)
                    {
                        currentValue *= _scoringPositiveScale;
                    }
                    else if (currentValue < 0)
                    {
                        currentValue *= _scoringNegativeScale;
                    }
                }
            }

            return currentValue;
        }

    }
}

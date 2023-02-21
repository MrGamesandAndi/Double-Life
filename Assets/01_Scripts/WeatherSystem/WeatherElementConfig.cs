using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WeatherSystem
{
    [Serializable]
    public class WeatherElementConfig
    {
        [Range(0f, 1f)] public float intensity = 0f;
        [Range(0f, 1f)] public float fluctutationAmount = 0f;

        public float minFluctuationInterval = 0f;
        public float maxFluctuationInterval = 0f;

        public float GetRandomIntensity()
        {
            if (intensity <= 0)
            {
                return 0f;
            }

            return Mathf.Clamp01(intensity + Random.Range(-fluctutationAmount, fluctutationAmount));
        }

        public float GetFluctuationTime()
        {
            return Random.Range(minFluctuationInterval, maxFluctuationInterval);
        }
    }
}
using UnityEngine;

namespace WeatherSystem
{
    [CreateAssetMenu(menuName = "Weather/Preset", fileName = "WeatherPreset_")]
    public class WeatherPreset : ScriptableObject
    {
        [Header("Global")]
        public float minFluctuationInterval = 0f;
        public float maxFluctuationInterval = 0f;
        [Range(0f, 1f)] public float fluctutationAmount = 0f;

        [Header("Individual Effects")]
        public WeatherElementConfig rain;
        public WeatherElementConfig hail;
        public WeatherElementConfig snow;

        public float GetFluctuationTime()
        {
            return Random.Range(minFluctuationInterval, maxFluctuationInterval);
        }

        public float GetRandomFluctuation()
        {
            if (fluctutationAmount <= 0)
            {
                return 0f;
            }

            return Random.Range(-fluctutationAmount, fluctutationAmount);
        }
    }
}
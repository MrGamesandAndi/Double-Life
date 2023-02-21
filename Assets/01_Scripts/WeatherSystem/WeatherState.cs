using UnityEngine;

namespace WeatherSystem
{
    public class WeatherState
    {
        public float initialIntensity;
        public float currentIntensity;
        public float targetIntensity;

        float _transitionProgress;
        float _transitionTime;

        bool _transitionInProgress = false;

        WeatherElementConfig _config;

        public void SwitchToNewPreset(WeatherElementConfig config, float transitionTime)
        {
            initialIntensity = currentIntensity;
            targetIntensity = config.GetRandomIntensity();
            _config = config;
            _transitionProgress = transitionTime > 0f ? 0f : 1f;
            _transitionTime = transitionTime;
            _transitionInProgress = true;
        }

        public float Tick()
        {
            if (!_transitionInProgress)
            {
                return currentIntensity;
            }

            if (_transitionProgress < 1f)
            {
                _transitionProgress += Time.deltaTime / _transitionTime;
            }

            currentIntensity = Mathf.Lerp(initialIntensity, targetIntensity, _transitionProgress);

            if (_transitionProgress >= 1f)
            {
                initialIntensity = currentIntensity;
                targetIntensity = _config.GetRandomIntensity();
                _transitionProgress = 0f;
                _transitionTime = _config.GetFluctuationTime();
                _transitionInProgress = _transitionTime > 0f;
            }

            return currentIntensity;
        }
    }
}
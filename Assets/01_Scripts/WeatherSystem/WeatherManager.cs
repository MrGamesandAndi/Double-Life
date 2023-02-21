using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

namespace WeatherSystem
{
    public class WeatherManager : MonoBehaviour
    {
        public static WeatherManager Instance { get; private set; }

        [Range(0f, 1f)]
        [SerializeField] float _rainIntensity;

        [Range(0f, 1f)]
        [SerializeField] float _snowIntensity;

        [Range(0f, 1f)]
        [SerializeField] float _hailIntensity;

        [SerializeField] VisualEffect _rainVFX;
        [SerializeField] VisualEffect _snowVFX;
        [SerializeField] VisualEffect _hailVFX;

        [Header("Default Preset")]
        [SerializeField] WeatherPreset _defaultWeather;

        [Header("Time of Day")]
        [SerializeField] float _timeMultiplier = 90f;
        [SerializeField] float _hoursPerDay = 24f;
        [SerializeField] float _sunriseTime = 6f;
        [SerializeField] float _sunsetTime = 18f;
        [SerializeField] Transform _solarSystem;
        [SerializeField] UniversalAdditionalLightData _sunLightData;
        [SerializeField] UniversalAdditionalLightData _moonLightData;

        [Header("Debug Only")]
        public bool performTransition;
        public WeatherPreset targetPreset;
        public float transitionTime;

        float _startTimeInHours;
        float _previousRainIntensity;
        float _previousHailIntensity;
        float _previousSnowIntensity;
        float _initialFluctuation = 0f;
        float _currentFluctuation = 0f;
        float _targetFluctuation = 0f;
        float _fluctuationTime = 0f;
        float _fluctuationProgress = 0f;
        float _currentTimeInSeconds = 0f;

        float CurrentTimeInHours => _currentTimeInSeconds / 3600f;
        float DayLenght => _sunsetTime - _sunriseTime;
        float NightLenght => _hoursPerDay - DayLenght;
        bool IsDay => CurrentTimeInHours >= _sunriseTime && CurrentTimeInHours <= _sunsetTime;
        bool IsNight => !IsDay;

        public WeatherPreset CurrentWeather { get => _currentWeather;}
        public float CurrentTime { get => CurrentTimeInHours; }

        bool _fluctuationInProgress = false;

        WeatherState _stateRain = new WeatherState();
        WeatherState _stateSnow = new WeatherState();
        WeatherState _stateHail = new WeatherState();
        WeatherPreset _currentWeather;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _startTimeInHours = DateTime.Now.Hour;
            _rainVFX.SetFloat("Intensity", _rainIntensity);
            _hailVFX.SetFloat("Intensity", _hailIntensity);
            _snowVFX.SetFloat("Intensity", _snowIntensity);
            ChangeWeather(_defaultWeather, 0f);
            _currentTimeInSeconds = _startTimeInHours * 3600f;
            UpdateTime();
        }


        private void Update()
        {
            if (performTransition)
            {
                performTransition = false;
                ChangeWeather(targetPreset, transitionTime);
            }

            UpdateTime();
            UpdateWeatherTransition();

            if (_rainIntensity != _previousRainIntensity)
            {
                _previousRainIntensity = _rainIntensity;
                _rainVFX.SetFloat("Intensity", _rainIntensity);
            }

            if (_hailIntensity != _previousHailIntensity)
            {
                _previousHailIntensity = _hailIntensity;
                _hailVFX.SetFloat("Intensity", _hailIntensity);
            }

            if (_snowIntensity != _previousSnowIntensity)
            {
                _previousSnowIntensity = _snowIntensity;
                _snowVFX.SetFloat("Intensity", _snowIntensity);
            }
        }

        public void ChangeWeather(WeatherPreset newWeather, float transitionTime)
        {
            _currentWeather = newWeather;
            _stateRain.SwitchToNewPreset(newWeather.rain, transitionTime);
            _stateHail.SwitchToNewPreset(newWeather.hail, transitionTime);
            _stateSnow.SwitchToNewPreset(newWeather.snow, transitionTime);
            _initialFluctuation = _currentFluctuation;
            _targetFluctuation = _currentWeather.GetRandomFluctuation();
            _fluctuationProgress = 0f;
            _fluctuationTime = _currentWeather.GetFluctuationTime();
            _fluctuationInProgress = _fluctuationTime > 0f;

            if (!_fluctuationInProgress)
            {
                _currentFluctuation = _initialFluctuation = _targetFluctuation = 0f;
            }
        }

        private void UpdateWeatherTransition()
        {
            if (_fluctuationInProgress)
            {
                _fluctuationProgress += Time.deltaTime / _fluctuationTime;
                _currentFluctuation = Mathf.Lerp(_initialFluctuation, _targetFluctuation, _fluctuationProgress);

                if (_fluctuationProgress >= 1f)
                {
                    _initialFluctuation = _currentFluctuation;
                    _targetFluctuation = _currentWeather.GetRandomFluctuation();
                    _fluctuationTime = _currentWeather.GetFluctuationTime();
                    _fluctuationProgress = 0f;
                }
            }

            _rainIntensity = Mathf.Clamp01(_currentFluctuation + _stateRain.Tick());
            _hailIntensity = Mathf.Clamp01(_currentFluctuation + _stateHail.Tick());
            _snowIntensity = Mathf.Clamp01(_currentFluctuation + _stateSnow.Tick());
        }

        private void UpdateTime()
        {
            _currentTimeInSeconds = (_currentTimeInSeconds + Time.deltaTime * _timeMultiplier) % (_hoursPerDay * 3600f);
            float solarAngle = 0f;

            if (IsDay)
            {
                solarAngle = 180f * Mathf.InverseLerp(_sunriseTime, _sunsetTime, CurrentTimeInHours);
            }
            else
            {
                float hoursIntoNight = 0f;

                if (CurrentTimeInHours > _sunsetTime)
                {
                    hoursIntoNight = CurrentTimeInHours - _sunsetTime;
                }
                else
                {
                    hoursIntoNight = CurrentTimeInHours + (_hoursPerDay - _sunsetTime);
                }

                solarAngle = 180f + 180f * (hoursIntoNight / NightLenght);
            }

            _solarSystem.eulerAngles = new Vector3(solarAngle, 0f, 0f);
        }
    }
}
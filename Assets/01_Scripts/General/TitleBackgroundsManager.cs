using UnityEngine;
using WeatherSystem;

namespace General
{
    public class TitleBackgroundsManager : MonoBehaviour
    {
        [Header("Time Of Day Backgrounds")]
        [SerializeField] GameObject _dayBG;
        [SerializeField] GameObject _sunriseBG;
        [SerializeField] GameObject _nightBG;
        [SerializeField] GameObject _sunsetBG;

        private void Start()
        {
            CheckForCurrentTime();
        }

        private void CheckForCurrentTime()
        {
            int currentTime = (int)WeatherManager.Instance.CurrentTime;

            if (currentTime >= 24 && currentTime <= 6)
            {
                _sunriseBG.SetActive(true);
            }

            if (currentTime >= 7 && currentTime <= 12)
            {
                _dayBG.SetActive(true);
            }

            if (currentTime >= 13 && currentTime <= 18)
            {
                _sunsetBG.SetActive(true);
            }

            if (currentTime >= 19 && currentTime <= 23)
            {
                _nightBG.SetActive(true);
            }
        }
    }
}
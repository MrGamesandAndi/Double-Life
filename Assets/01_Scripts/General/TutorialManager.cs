using SaveSystem;
using SceneManagement;
using TMPro;
using UnityEngine;

namespace General
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] GameObject _cityNamePrompt;
        [SerializeField] TMP_InputField _cityNameInputField;

        private void ShowCityNamePrompt()
        {
            _cityNamePrompt.SetActive(true);
        }

        public void SetCityName()
        {
            var valueToSet = _cityNameInputField.text;

            if (string.IsNullOrEmpty(valueToSet))
            {
                return;
            }

            SaveManager.Instance.PlayerData.cityName = valueToSet;
            _cityNamePrompt.SetActive(false);
        }

        private void OpenDoubleCreator()
        {
            PlayerPrefs.SetInt("CC_State", 2);
            ScenesManager.Instance.LoadScene(Scenes.Character_Creator, Scenes.Tutorial);
        }

        private void OpenCity()
        {
            ScenesManager.Instance.LoadScene(Scenes.City, Scenes.Tutorial);
        }
    }
}

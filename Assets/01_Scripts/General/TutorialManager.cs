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
        [SerializeField] DialogueTrigger _dialogueTrigger1;
        [SerializeField] DialogueTrigger _dialogueTrigger2;

        private void Start()
        {
            _dialogueTrigger1.InitiateDialogue();
        }

        public void ShowCityNamePrompt()
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

        public void OpenDoubleCreator()
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

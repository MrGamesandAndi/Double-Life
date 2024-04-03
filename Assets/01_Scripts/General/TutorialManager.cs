using AudioSystem;
using SaveSystem;
using SceneManagement;
using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace General
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] GameObject _cityNamePrompt;
        [SerializeField] TMP_InputField _cityNameInputField;

        DialogueRunner _dialogueRunner;
        private void Awake()
        {
            _dialogueRunner = FindObjectOfType<DialogueRunner>();
        }

        private void Start()
        {
            _dialogueRunner.StartDialogue("Tutorial1");
        }

        [YarnCommand("city_name_prompt")]
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

            _dialogueRunner.VariableStorage.SetValue("$cityName", valueToSet);
            SaveManager.Instance.PlayerData.cityName = valueToSet;
            _cityNamePrompt.SetActive(false);
            _dialogueRunner.StartDialogue("Tutorial3");
        }

        [YarnCommand("enter_double_creator")]
        private void OpenDoubleCreator()
        {
            PlayerPrefs.SetInt("CC_State", 2);
            ScenesManager.Instance.LoadScene(Scenes.Character_Creator, Scenes.Tutorial);
        }

        [YarnCommand("go_to_city")]
        private void OpenCity()
        {
            ScenesManager.Instance.LoadScene(Scenes.City, Scenes.Tutorial);
        }
    }
}

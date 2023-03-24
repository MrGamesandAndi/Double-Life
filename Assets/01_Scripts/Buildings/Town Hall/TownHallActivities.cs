using SaveSystem;
using SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownHall
{
    public class TownHallActivities : MonoBehaviour
    {
        [Header("City Name")]
        [SerializeField] TMP_InputField _inputField;
        [SerializeField] Button _chanceCityNameButton;

        public void GoToCharacterCreator()
        {
            ScenesManager.Instance.LoadScene(Scenes.Character_Creator, Scenes.Town_Hall);
        }

        public void ChangeName()
        {
            if (!string.IsNullOrEmpty(_inputField.text))
            {
                SaveManager.Instance.PlayerData.cityName = _inputField.text;
            }
        }

        public void CheckForValidString()
        {
            if (string.IsNullOrEmpty(_inputField.text))
            {
                _chanceCityNameButton.interactable = false;
            }
            else
            {
                _chanceCityNameButton.interactable = true;
            }
        }
    }
}

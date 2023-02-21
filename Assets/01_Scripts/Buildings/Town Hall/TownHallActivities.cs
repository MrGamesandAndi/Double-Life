using SaveSystem;
using SceneManagement;
using TMPro;
using UnityEngine;

namespace TownHall
{
    public class TownHallActivities : MonoBehaviour
    {
        [Header("City Name")]
        [SerializeField] TMP_InputField _inputField;

        public void GoToCharacterCreator()
        {
            ScenesManager.Instance.LoadScene(Scenes.Character_Creator, Scenes.Town_Hall);
        }

        public void ChangeName()
        {
            SaveManager.Instance.PlayerData.cityName = _inputField.text;
        }
    }
}

using Localisation;
using SaveSystem;
using SceneManagement;
using UnityEngine;

namespace General
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] GameObject _englishTouchText;
        [SerializeField] GameObject _spanishTouchText;
        [SerializeField] GameObject _englishLogoText;
        [SerializeField] GameObject _spanishLogoText;

        private void Start()
        {
            if (SaveManager.Instance.PlayerData.language == Language.English)
            {
                _englishLogoText.SetActive(true);
                _englishTouchText.SetActive(true);
            }
            else
            {
                _spanishLogoText.SetActive(true);
                _spanishTouchText.SetActive(true);
            }

            LocalisationSystem.language = SaveManager.Instance.PlayerData.language;

            if (SaveManager.Instance.PlayerData.isOnTutorial)
            {
                GetComponent<SceneLoader>().sceneToLoad = Scenes.Tutorial;
            }
            else
            {
                GetComponent<SceneLoader>().sceneToLoad = Scenes.City;
            }
        }
    }
}

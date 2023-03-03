using Localisation;
using SaveSystem;
using UnityEngine;
using Yarn.Unity;

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
            DialogueRunner tutorialDialogue = FindObjectOfType<DialogueRunner>();

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
                tutorialDialogue.GetComponent<TextLineProvider>().textLanguageCode = "en";
                GetComponent<SceneLoader>().sceneToLoad = SceneManagement.Scenes.Tutorial;
            }
            else
            {
                tutorialDialogue.gameObject.SetActive(false);
                //GetComponent<SceneLoader>().sceneToLoad = SceneManagement.Scenes.City;
                GetComponent<SceneLoader>().sceneToLoad = SceneManagement.Scenes.RPG_Minigame;
            }
        }
    }
}

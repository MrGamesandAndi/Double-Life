using Localisation;
using SaveSystem;
using SceneManagement;
using UnityEngine;

namespace Buildings
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] Scenes _scene;
        [SerializeField] string _flag;
        [SerializeField] bool _usesAchievements;
        [SerializeField] LocalisedString _content;
        [SerializeField] Sprite _spanishTitleCard;
        [SerializeField] Sprite _englishTitleCard;


        bool _found = false;

        private void Start()
        {
            if (_usesAchievements)
            {
                for (int i = 0; i < AchievementManager.instance.AchievementList.Count; i++)
                {
                    if (AchievementManager.instance.AchievementList[i].Key == _flag)
                    {
                        if (AchievementManager.instance.States[i].Achieved == true)
                        {
                            gameObject.SetActive(true);
                            _found = true;
                            break;
                        }
                    }
                }

                if (!_found)
                {
                    gameObject.SetActive(false);
                }
            }
        }


        public void LoadScene()
        {
            ScenesManager.Instance.LoadScene(_scene, Scenes.City);
        }

        private void OnMouseDown()
        {
            GetComponent<BoxCollider>().enabled = false;

            if (SaveManager.Instance.PlayerData.language == Language.Spanish)
            {
                BuildingsDescription.Instance.ShowDescription(_content.Value, _spanishTitleCard, LoadScene);
            }
            else
            {
                BuildingsDescription.Instance.ShowDescription(_content.Value, _englishTitleCard, LoadScene);
            }
        }
    }
}
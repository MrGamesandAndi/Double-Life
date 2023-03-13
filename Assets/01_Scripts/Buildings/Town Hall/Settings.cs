using AudioSystem;
using General;
using Localisation;
using SaveSystem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TownHall
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] LocalisedString _changeLanguagePrompt;
        [SerializeField] TMP_Dropdown _resolutionsDropdown;
        [SerializeField] TMP_Dropdown _languagesDropdown;
        [SerializeField] Slider _musicSlider;

        Resolution[] _resolutions;
        int currentIndex;

        private void Start()
        {
            GetResolutions();
            GetLanguages();
            _musicSlider.value = SaveManager.Instance.PlayerData.musicVolume;
        }

        private void GetResolutions()
        {
            _resolutions = Screen.resolutions;
            _resolutionsDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + "x" + _resolutions[i].height;
                options.Add(option);

                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            _resolutionsDropdown.AddOptions(options);
            _resolutionsDropdown.value = currentResolutionIndex;
            _resolutionsDropdown.RefreshShownValue();
        }

        private void GetLanguages()
        {
            _languagesDropdown.ClearOptions();
            List<string> languages = new List<string>();
            currentIndex = 0;

            foreach (Language language in (Language[])Enum.GetValues(typeof(Language)))
            {
                languages.Add(language.ToString());

                if(language == SaveManager.Instance.PlayerData.language)
                {
                    currentIndex = (int)language;
                }
            }

            _languagesDropdown.AddOptions(languages);
            _languagesDropdown.value = currentIndex;
            _languagesDropdown.RefreshShownValue();
            _languagesDropdown.onValueChanged.AddListener(delegate
            {
                SetLanguage();
            });
        }

        public void SetVolume(float volume)
        {
            AudioManager.Instance.SetMusicVolume(volume);
            AudioManager.Instance.SetSfxVolume(volume);
            SaveManager.Instance.PlayerData.musicVolume = volume;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            SaveManager.Instance.PlayerData.resolutionX = resolution.width;
            SaveManager.Instance.PlayerData.resolutionY = resolution.height;
        }

        public void SetLanguage()
        {
            SaveManager.Instance.PlayerData.language = (Language)_languagesDropdown.value;
            ModalWindow.Instance.ShowQuestion(_changeLanguagePrompt.Value, () => 
            {
                SaveManager.Instance.SaveAllData();
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#endif
            }, () => 
            {
                _languagesDropdown.value = currentIndex;
                ModalWindow.Instance.Hide();
            });
        }
    }
}

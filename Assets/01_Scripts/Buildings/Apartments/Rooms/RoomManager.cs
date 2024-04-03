using AudioSystem;
using Buildings.ShopSystem;
using CameraSystem.RoomView;
using General;
using Localisation;
using SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;


namespace Buildings.Apartments.Rooms
{
    public class RoomManager : MonoBehaviour
    {

        [SerializeField] AudioClip _coinSFX;

        public static RoomManager Instance { get; private set; }
        public FurnitureItem SelectedFurniture { get => _selectedFurniture; set => _selectedFurniture = value; }
        public DialogueRunner DialogueRunner { get => _dialogueRunner; set => _dialogueRunner = value; }

        FurnitureItem _selectedFurniture = null;

        [SerializeField] Camera _camera;
        [SerializeField] GameObject _tabArea;
        [SerializeField] GameObject _mainPanel;
        [SerializeField] GameObject _pagesArea;
        [SerializeField] GameObject _buildArea;
        [SerializeField] Material _wallMaterial;
        [SerializeField] DialogueRunner _dialogueRunner;
        [SerializeField] List<AudioClip> _moodMusicList;
        [SerializeField] List<YarnProject> _moodDialogueList;
        [SerializeField] GameObject _speechBubble;
        [SerializeField] TextMeshProUGUI _moneyText;
        [SerializeField] GameObject _instructions;
        [SerializeField] GameObject DEBUG_needPanel;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _wallMaterial.color = GameManager.Instance.currentLoadedDouble.Color;

            if (SaveManager.Instance.PlayerData.language == Language.Spanish)
            {
                _dialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = "es-BO";
            }
            else
            {
                _dialogueRunner.GetComponent<TextLineProvider>().textLanguageCode = "en";
            }

            switch (GameManager.Instance.currentLoadedDouble.CurrentState)
            {
                case DoubleState.Happy:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    _dialogueRunner.SetProject(_moodDialogueList[0]);
                    break;
                case DoubleState.Buy:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    _dialogueRunner.SetProject(_moodDialogueList[2]);
                    break;
                case DoubleState.MakeFriend:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    _dialogueRunner.SetProject(_moodDialogueList[6]);
                    break;
                case DoubleState.Confession:
                    AudioManager.Instance.PlayMusic(_moodMusicList[1]);
                    _dialogueRunner.SetProject(_moodDialogueList[1]);
                    break;
                case DoubleState.Angry:
                    AudioManager.Instance.PlayMusic(_moodMusicList[2]);
                    _dialogueRunner.SetProject(_moodDialogueList[7]);
                    break;
                case DoubleState.Sick:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    _dialogueRunner.SetProject(_moodDialogueList[4]);
                    break;
                case DoubleState.Date:
                    AudioManager.Instance.PlayMusic(_moodMusicList[3]);
                    _dialogueRunner.SetProject(_moodDialogueList[1]);
                    break;
                case DoubleState.Hungry:
                    AchievementManager.instance.Unlock("Unlock_Supermarket");
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    _dialogueRunner.SetProject(_moodDialogueList[3]);
                    break;
                case DoubleState.Sad:
                    AudioManager.Instance.PlayMusic(_moodMusicList[4]);
                    _dialogueRunner.SetProject(_moodDialogueList[5]);
                    break;
                case DoubleState.BreakUp:
                    AudioManager.Instance.PlayMusic(_moodMusicList[4]);
                    _dialogueRunner.SetProject(_moodDialogueList[8]);
                    break;
                default:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    _dialogueRunner.SetProject(_moodDialogueList[0]);
                    Debug.Log("Reached default");
                    break;
            }

            _moneyText.text = $"$ {(GameManager.Instance.GetCurrentFunds() / 100f):0.00}";
            GameManager.Instance.OnMoneyChange += UpdateMoneyText;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnMoneyChange -= UpdateMoneyText;
        }

        public void UpdateMoneyText(int oldValue, int newValue)
        {
            StartCoroutine(CountTo(oldValue,newValue));
        }

        private IEnumerator CountTo(int oldValue, int newValue)
        {
            float countDuration = 1f;
            var rate = Mathf.Abs(newValue - oldValue) / countDuration;

            while (oldValue != newValue)
            {
                AudioManager.Instance.PlaySfx(_coinSFX);
                oldValue = (int)Mathf.MoveTowards(oldValue, newValue, rate * Time.deltaTime);
                _moneyText.text = $"$ {(oldValue / 100f):0.00}";
                yield return null;
            }
        }

        public void EnableGrid()
        {
            HideTabs();
            ChangeCameraAngle(CameraPresets.Top);
        }

        public void DisableGrid()
        {
            ChangeCameraAngle(CameraPresets.Front);
            _buildArea.SetActive(false);
            ShowTabs();
        }

        public void ChangeCameraAngle(CameraPresets preset)
        {
            _camera.GetComponent<RoomCameraManager>().MoveToPreset(preset);
        }

        public void ShowTabs()
        {
            _mainPanel.SetActive(true);
            _tabArea.SetActive(true);
            _pagesArea.SetActive(false);
            HideInstructions();
        }

        public void HideTabs()
        {
            _mainPanel.SetActive(false);
            _tabArea.SetActive(false);
            _pagesArea.SetActive(false);
            _speechBubble.SetActive(false);
            HideInstructions();
        }

        public void ResetSelectedFurniture()
        {
            _selectedFurniture = null;
        }

        public void ShowInstructions()
        {
            _instructions.SetActive(true);
        }

        public void HideInstructions()
        {
            _instructions.SetActive(false);
        }

        public void DEBUG_ShowNeeds(bool state)
        {
            DEBUG_needPanel.SetActive(state);
        }
    }
}
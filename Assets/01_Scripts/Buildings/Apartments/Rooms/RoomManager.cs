using AudioSystem;
using Buildings.ShopSystem;
using CameraSystem.RoomView;
using General;
using Localisation;
using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Buildings.Apartments.Rooms
{
    public class RoomManager : MonoBehaviour
    {

        [SerializeField] AudioClip _coinSFX;

        public static RoomManager Instance { get; private set; }
        public FurnitureItem SelectedFurniture { get => _selectedFurniture; set => _selectedFurniture = value; }

        FurnitureItem _selectedFurniture = null;

        [SerializeField] Camera _camera;
        [SerializeField] GameObject _tabArea;
        [SerializeField] GameObject _mainPanel;
        [SerializeField] GameObject _pagesArea;
        [SerializeField] GameObject _buildArea;
        [SerializeField] Material _wallMaterial;
        [SerializeField] List<AudioClip> _moodMusicList;
        [SerializeField] GameObject _speechBubble;
        [SerializeField] TextMeshProUGUI _moneyText;
        [SerializeField] GameObject DEBUG_needPanel;
        [SerializeField] GameObject _quitButton;
        [SerializeField] GameObject _friendshipButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _wallMaterial.color = GameManager.Instance.currentLoadedDouble.Color;

            if (SaveManager.Instance.PlayerData.language == Language.Spanish)
            {
                
            }
            else
            {
                
            }

            switch (GameManager.Instance.currentLoadedDouble.CurrentState)
            {
                case DoubleState.Happy:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    
                    break;
                case DoubleState.Buy:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case DoubleState.MakeFriend:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case DoubleState.Confession:
                    AudioManager.Instance.PlayMusic(_moodMusicList[1]);
                    break;
                case DoubleState.Angry:
                    AudioManager.Instance.PlayMusic(_moodMusicList[2]);
                    break;
                case DoubleState.Sick:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case DoubleState.Date:
                    AudioManager.Instance.PlayMusic(_moodMusicList[3]);
                    break;
                case DoubleState.Hungry:
                    AchievementManager.instance.Unlock("Unlock_Supermarket");
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case DoubleState.Sad:
                    AudioManager.Instance.PlayMusic(_moodMusicList[4]);
                    break;
                case DoubleState.BreakUp:
                    AudioManager.Instance.PlayMusic(_moodMusicList[4]);
                    break;
                default:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
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
            _quitButton.SetActive(true);
            _friendshipButton.SetActive(true);
        }

        public void HideTabs()
        {
            _mainPanel.SetActive(false);
            _tabArea.SetActive(false);
            _pagesArea.SetActive(false);
            _speechBubble.SetActive(false);
            _quitButton.SetActive(false);
            _friendshipButton.SetActive(false);
        }

        public void ResetSelectedFurniture()
        {
            _selectedFurniture = null;
        }

        public void DEBUG_ShowNeeds(bool state)
        {
            DEBUG_needPanel.SetActive(state);
        }
    }
}
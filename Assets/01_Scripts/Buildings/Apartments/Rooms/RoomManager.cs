using AudioSystem;
using Buildings.ShopSystem;
using CameraSystem.RoomView;
using General;
using Needs;
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

            switch (GameManager.Instance.currentLoadedDouble.CurrentState)
            {
                case NeedType.Happy:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case NeedType.BuyFurniture:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case NeedType.MakeFriend:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case NeedType.TalkToFriend:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case NeedType.ConfessLove:
                    AudioManager.Instance.PlayMusic(_moodMusicList[1]);
                    break;
                case NeedType.HaveFight:
                    AudioManager.Instance.PlayMusic(_moodMusicList[2]);
                    break;
                case NeedType.Sickness:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case NeedType.HaveDate:
                    AudioManager.Instance.PlayMusic(_moodMusicList[3]);
                    break;
                case NeedType.Hunger:
                    AchievementManager.instance.Unlock("Unlock_Supermarket");
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    break;
                case NeedType.HaveDepression:
                    AudioManager.Instance.PlayMusic(_moodMusicList[4]);
                    break;
                case NeedType.BreakUp:
                    AudioManager.Instance.PlayMusic(_moodMusicList[4]);
                    break;
                default:
                    AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                    Debug.Log("Reached default");
                    ShowTabs();
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
            LeanTween.value(oldValue, newValue, 1f).setOnUpdate((float val) => 
            { 
                AudioManager.Instance.PlaySfx(_coinSFX);
                _moneyText.text = $"$ {(val / 100f):0.00}";
            });
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
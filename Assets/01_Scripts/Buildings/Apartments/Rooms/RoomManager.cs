using Apartments;
using AudioSystem;
using General;
using Localisation;
using Needs;
using SaveSystem;
using ShopSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }
    public FurnitureItem SelectedFurniture { get => _selectedFurniture; set => _selectedFurniture = value; }

    public FurnitureItem _selectedFurniture;

    [SerializeField] GameObject _grid;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _tabArea;
    [SerializeField] GameObject _mainPanel;
    [SerializeField] GameObject _pagesArea;
    [SerializeField] Material _wallMaterial;
    [SerializeField] GameObject _language;
    [SerializeField] List<AudioClip> _moodMusicList;
    [SerializeField] List<YarnProject> _moodDialogueList;
    [SerializeField] GameObject _speechBubble;
    [SerializeField] TextMeshProUGUI _moneyText;
    public GameObject humanModel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _wallMaterial.color = GameManager.Instance.currentLoadedDouble.Color;

        if (SaveManager.Instance.PlayerData.language == Language.Spanish)
        {
            _language.GetComponent<TextLineProvider>().textLanguageCode = "es-BO";
        }
        else
        {
            _language.GetComponent<TextLineProvider>().textLanguageCode = "en";
        }

        switch (GameManager.Instance.currentState)
        {
            case DoubleState.Happy:
                AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[0]);
                break;
            case DoubleState.Buy:
                AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[2]);
                break;
            case DoubleState.MakeFriend:
                AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[6]);
                break;
            case DoubleState.Confession:
                AudioManager.Instance.PlayMusic(_moodMusicList[1]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[1]);
                break;
            case DoubleState.Angry:
                AudioManager.Instance.PlayMusic(_moodMusicList[2]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[7]);
                break;
            case DoubleState.Sick:
                AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[4]);
                break;
            case DoubleState.Date:
                AudioManager.Instance.PlayMusic(_moodMusicList[3]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[1]);
                break;
            case DoubleState.Hungry:
                AchievementManager.instance.Unlock("Unlock_Supermarket");
                AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[3]);
                break;
            case DoubleState.Sad:
                AudioManager.Instance.PlayMusic(_moodMusicList[4]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[5]);
                break;
            default:
                AudioManager.Instance.PlayMusic(_moodMusicList[0]);
                _language.GetComponent<DialogueRunner>().SetProject(_moodDialogueList[0]);
                Debug.Log("Reached default");
                break;
        }

        UpdateMoneyText();

    }

    public void UpdateMoneyText()
    {
        _moneyText.text = $"$ {(GameManager.Instance.GetCurrentFunds() / 100f):0.00}";
    }

    public void EnableGrid()
    {
        _grid.SetActive(true);
        ChangeCameraAngle(CameraPresets.Top);
    }

    public void DisableGrid()
    {
        ChangeCameraAngle(CameraPresets.Front);
        _grid.SetActive(false);
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
    }

    public void HideTabs()
    {
        _mainPanel.SetActive(false);
        _tabArea.SetActive(false);
        _pagesArea.SetActive(false);
        _speechBubble.SetActive(false);
    }
}

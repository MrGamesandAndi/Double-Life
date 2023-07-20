using Buildings.ShopSystem;
using General;
using SaveSystem;
using SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings.Apartments.Rooms
{
    public class TreasureUI : MonoBehaviour
    {
        [SerializeField] Transform _itemUIRoot;
        [SerializeField] GameObject _itemUIPrefab;
        [SerializeField] Button _giveButton;
        [SerializeField] GameObject _treasurePrefab;
        [SerializeField] Transform _treasureSpawnArea;
        [SerializeField] GameObject _snapPoint;

        Dictionary<Treasures, TreasureUIItem> _treasureItemToUIMap = new Dictionary<Treasures, TreasureUIItem>();
        List<Treasures> _orderedList = new List<Treasures>();
        Treasure _selectedItem;
        List<Treasures> _availableItems;

        private void Awake()
        {
            GetPlayerTreasure();
        }

        private void OnEnable()
        {
            ResetAllButtons();
            RefreshUITreasures();
        }

        private void GetPlayerTreasure()
        {
            _availableItems = new List<Treasures>();

            if (SaveManager.Instance.PlayerData.obtainedTreasures.Length != 0)
            {
                foreach (var item in SaveManager.Instance.PlayerData.obtainedTreasures)
                {
                    if (BodyPartsCollection.Instance.ReturnTreasure(item.id).canBeGiven && item.amount > 0)
                    {
                        _availableItems.Add(item);
                    }
                }
            }
        }

        public void RefreshUICommon()
        {
            _giveButton.GetComponent<Button>().onClick.RemoveAllListeners();

            if (_selectedItem != null)
            {
                _giveButton.interactable = true;
                _giveButton.GetComponent<Button>().onClick.AddListener(OnClickedGive);
            }
            else
            {
                _giveButton.interactable = false;
            }
        }

        public void RefreshUITreasures()
        {
            GetPlayerTreasure();
            _treasureItemToUIMap.Clear();
            _orderedList.Clear();
            _orderedList = _availableItems.OrderBy(o => o.id).ToList();

            foreach (var furniture in _orderedList)
            {
                var itemGO = Instantiate(_itemUIPrefab, _itemUIRoot);
                var itemUI = itemGO.GetComponent<TreasureUIItem>();
                itemUI.Bind(BodyPartsCollection.Instance.ReturnTreasure(furniture.id), BodyPartsCollection.Instance.ReturnPlayerTreasure(furniture.id).amount, OnItemSelected);
                _treasureItemToUIMap[furniture] = itemUI;
            }

            RefreshUICommon();
        }

        public void OnClickedGive()
        {
            _treasurePrefab.GetComponent<SpriteRenderer>().sprite = _selectedItem.icon;
            _treasurePrefab.GetComponent<TreasureDragInteraction>()._snapPoint = _snapPoint;
            _treasurePrefab.GetComponent<TreasureDragInteraction>().id = _selectedItem.id;

            if (_selectedItem.scene != Scenes.Loading_Screen)
            {
                _treasurePrefab.GetComponent<TreasureDragInteraction>()._sceneToLoad = _selectedItem.scene;
            }

            Instantiate(_treasurePrefab, _treasureSpawnArea);
            RoomManager.Instance.HideTabs();
            GameManager.Instance.SpendTreasure(_selectedItem.id, 1);
        }

        private void ResetAllButtons()
        {
            foreach (var kvp in _treasureItemToUIMap)
            {
                var itemUI = kvp.Value;
                itemUI.SetIsSelected(false);
            }

            foreach (Transform child in _itemUIRoot)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnItemSelected(Treasure newlySelectedItem)
        {
            _selectedItem = newlySelectedItem;

            foreach (var kvp in _treasureItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;

                itemUI.SetIsSelected(item.id == BodyPartsCollection.Instance.ReturnPlayerTreasure(_selectedItem.id).id);
            }

            RefreshUICommon();
        }

        public void AddGiveButtonListener()
        {
            _giveButton.GetComponent<Button>().onClick.AddListener(OnClickedGive);
        }

        public void RemoveGiveButtonListener()
        {
            _giveButton.GetComponent<Button>().onClick.RemoveListener(OnClickedGive);
        }
    }
}
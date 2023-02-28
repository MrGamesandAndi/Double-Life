using SaveSystem;
using ShopSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Apartments
{
    public class FoodUI : MonoBehaviour
    {
        [SerializeField] Transform _itemUIRoot;
        [SerializeField] GameObject _itemUIPrefab;
        [SerializeField] List<FoodItem> _availableItems;
        [SerializeField] Transform _foodSpawnArea;
        [SerializeField] GameObject _foodPrefab;
        [SerializeField] GameObject _snapPoint;
        [SerializeField] Button _giveButton;

        Dictionary<FoodItem, FoodUIItem> _foodItemToUIMap = new Dictionary<FoodItem, FoodUIItem>();
        List<FoodItem> _orderedList = new List<FoodItem>();
        FoodItem _selectedItem;

        private void OnEnable()
        {
            RefreshUIProducts();
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

        public void RefreshUIProducts()
        {
            _foodItemToUIMap.Clear();
            _orderedList.Clear();
            _orderedList = _availableItems.OrderBy(o => o.foodName.Value).ToList();

            foreach (var food in _orderedList)
            {
                for (int i = 0; i < SaveManager.Instance.FoodData.Count; i++)
                {
                    if(SaveManager.Instance.FoodData[i].amount > 0)
                    {
                        if (food.foodName.key == SaveManager.Instance.FoodData[i].foodName.key)
                        {
                            var itemGO = Instantiate(_itemUIPrefab, _itemUIRoot);
                            var itemUI = itemGO.GetComponent<FoodUIItem>();
                            itemUI.Bind(food, SaveManager.Instance.FoodData[i].amount, OnItemSelected);
                            _foodItemToUIMap[food] = itemUI;
                            break;
                        }
                    }
                }
            }

            RefreshUICommon();
        }

        public void OnClickedGive()
        {
            _foodPrefab.GetComponent<SpriteRenderer>().sprite = _selectedItem.icon;
            _foodPrefab.GetComponent<DragInteraction>()._snapPoint = _snapPoint;
            _foodPrefab.GetComponent<DragInteraction>().expReward = _selectedItem.experienceReward;
            Instantiate(_foodPrefab, _foodSpawnArea);

            for (int i = 0; i < SaveManager.Instance.FoodData.Count; i++)
            {
                if (SaveManager.Instance.FoodData[i].foodName.key == _selectedItem.foodName.key)
                {
                    SaveManager.Instance.FoodData[i].amount--;
                    break;
                }
            }

            RoomManager.Instance.HideTabs();
            ResetAllButtons();
        }

        private void ResetAllButtons()
        {
            foreach (var kvp in _foodItemToUIMap)
            {
                var itemUI = kvp.Value;
                itemUI.SetIsSelected(false);
            }

            _selectedItem = null;

            foreach (Transform child in _itemUIRoot)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnItemSelected(FoodItem newlySelectedItem)
        {
            _selectedItem = newlySelectedItem;

            foreach (var kvp in _foodItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;
                itemUI.SetIsSelected(item == _selectedItem);
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

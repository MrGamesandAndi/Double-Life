using Buildings.ShopSystem;
using SaveSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings.Apartments.Rooms
{
    public class FurnitureUI : MonoBehaviour
    {
        [SerializeField] Transform _itemUIRoot;
        [SerializeField] GameObject _itemUIPrefab;
        [SerializeField] List<FurnitureItem> _availableItems;
        [SerializeField] Button _giveButton;

        Dictionary<FurnitureItem, FurnitureUIItem> _furnitureItemToUIMap = new Dictionary<FurnitureItem, FurnitureUIItem>();
        List<FurnitureItem> _orderedList = new List<FurnitureItem>();
        public FurnitureItem _selectedItem;

        private void OnEnable()
        {
            ResetAllButtons();
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
            _furnitureItemToUIMap.Clear();
            _orderedList.Clear();
            _orderedList = _availableItems.OrderBy(o => o.furnitureName.Value).ToList();

            foreach (var furniture in _orderedList)
            {
                for (int i = 0; i < SaveManager.Instance.FurnitureData.Count; i++)
                {
                    if (SaveManager.Instance.FurnitureData[i].amount > 0)
                    {
                        if (furniture.furnitureName.key == SaveManager.Instance.FurnitureData[i].itemName.key)
                        {
                            var itemGO = Instantiate(_itemUIPrefab, _itemUIRoot);
                            var itemUI = itemGO.GetComponent<FurnitureUIItem>();
                            itemUI.Bind(furniture, SaveManager.Instance.FurnitureData[i].amount, OnItemSelected);
                            _furnitureItemToUIMap[furniture] = itemUI;
                            break;
                        }
                    }
                }
            }

            RefreshUICommon();
        }

        public void OnClickedGive()
        {
            RoomManager.Instance.HideTabs();

            for (int i = 0; i < SaveManager.Instance.FurnitureData.Count; i++)
            {
                if (SaveManager.Instance.FurnitureData[i].itemName.key == _selectedItem.furnitureName.key)
                {
                    SaveManager.Instance.FurnitureData[i].amount--;
                    RoomManager.Instance.SelectedFurniture = _selectedItem;
                    break;
                }
            }

            RoomManager.Instance.EnableGrid();
            RoomManager.Instance.ShowInstructions();
            GridBuildingSystem3D.Instance.SetFurniture();
        }

        private void ResetAllButtons()
        {
            foreach (var kvp in _furnitureItemToUIMap)
            {
                var itemUI = kvp.Value;
                itemUI.SetIsSelected(false);
            }

            foreach (Transform child in _itemUIRoot)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnItemSelected(FurnitureItem newlySelectedItem)
        {
            _selectedItem = newlySelectedItem;

            foreach (var kvp in _furnitureItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;

                itemUI.SetIsSelected(item == _selectedItem);
            }

            RefreshUICommon();
        }
    }
}

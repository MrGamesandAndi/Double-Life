using General;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings.ShopSystem
{
    public class FurnitureShopUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _availableFundsText;
        [SerializeField] Transform _itemUIRoot;
        [SerializeField] GameObject _itemUIPrefab;
        [SerializeField] Button _purchaseButton;

        private Dictionary<FurnitureItem, FurnitureShopUIItem> furnitureItemToUIMap;
        private FurnitureItem selectedItem;

        private void Start()
        {
            RefreshUIProducts();
        }

        public void RefreshUICommon()
        {
            _availableFundsText.text = "";
            _availableFundsText.text = $"{(GameManager.Instance.GetCurrentFunds() / 100f):0.00}";
            _purchaseButton.GetComponent<Button>().onClick.RemoveAllListeners();

            if (selectedItem != null && GameManager.Instance.GetCurrentFunds() >= selectedItem.cost)
            {
                _purchaseButton.interactable = true;
                _purchaseButton.GetComponent<Button>().onClick.AddListener(OnClickedPurchase);

            }
            else
            {
                _purchaseButton.interactable = false;
            }

            foreach (var kvp in furnitureItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;
                itemUI.SetCanAfford(item.cost <= GameManager.Instance.GetCurrentFunds());
            }
        }

        public void RefreshUIProducts()
        {
            furnitureItemToUIMap = new Dictionary<FurnitureItem, FurnitureShopUIItem>();
            List<FurnitureItem> orderedList = BodyPartsCollection.Instance.furniture.OrderBy(o => o.furnitureName.Value).ToList();

            foreach (var item in orderedList)
            {
                var itemGO = Instantiate(_itemUIPrefab, _itemUIRoot);
                var itemUI = itemGO.GetComponent<FurnitureShopUIItem>();
                itemUI.Bind(item, OnItemSelected);
                furnitureItemToUIMap[item] = itemUI;
            }

            RefreshUICommon();
        }

        public void OnClickedPurchase()
        {
            GameManager.Instance.SpendFundsForFurniture(selectedItem);
            RefreshUICommon();
        }

        private void OnItemSelected(FurnitureItem newlySelectedItem)
        {
            selectedItem = newlySelectedItem;

            foreach (var kvp in furnitureItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;

                itemUI.SetIsSelected(item == selectedItem);
            }

            RefreshUICommon();
        }
    }
}

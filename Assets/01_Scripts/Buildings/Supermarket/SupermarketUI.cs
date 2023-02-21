using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class SupermarketUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _availableFundsText;
        [SerializeField] Transform _itemUIRoot;
        [SerializeField] GameObject _itemUIPrefab;
        [SerializeField] List<FoodItem> _availableItems;
        [SerializeField] Button _purchaseButton;

        private Dictionary<FoodItem, SupermarketUIItem> foodItemToUIMap;
        private FoodItem selectedItem;

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

            foreach (var kvp in foodItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;
                itemUI.SetCanAfford(item.cost <= GameManager.Instance.GetCurrentFunds());
            }
        }

        public void RefreshUIProducts()
        {
            foodItemToUIMap = new Dictionary<FoodItem, SupermarketUIItem>();
            List<FoodItem> orderedList = _availableItems.OrderBy(o => o.foodName.Value).ToList();

            foreach (var item in orderedList)
            {
                var itemGO = Instantiate(_itemUIPrefab, _itemUIRoot);
                var itemUI = itemGO.GetComponent<SupermarketUIItem>();
                itemUI.Bind(item, OnItemSelected);
                foodItemToUIMap[item] = itemUI;
            }

            RefreshUICommon();
        }

        public void OnClickedPurchase()
        {
            GameManager.Instance.SpendFundsForFood(selectedItem);
            RefreshUICommon();
        }

        private void OnItemSelected(FoodItem newlySelectedItem)
        {
            selectedItem = newlySelectedItem;

            foreach (var kvp in foodItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;

                itemUI.SetIsSelected(item == selectedItem);
            }

            RefreshUICommon();
        }
    }
}

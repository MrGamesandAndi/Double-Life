using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class PawnShopUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _availableFundsText;
        [SerializeField] Transform _itemUIRoot;
        [SerializeField] GameObject _itemUIPrefab;
        [SerializeField] List<Treasure> _availableItems;
        [SerializeField] Button _sellButton;

        Dictionary<Treasure, PawnShopUIItem> _treasureItemToUIMap;
        Treasure _selectedItem;

        private void Start()
        {
            RefreshUIProducts();
        }

        public void RefreshUICommon()
        {
            _availableFundsText.text = "";
            _availableFundsText.text = $"{(GameManager.Instance.GetCurrentFunds() / 100f):0.00}";
            _sellButton.GetComponent<Button>().onClick.RemoveAllListeners();

            if (_selectedItem != null)
            {
                _sellButton.GetComponent<Button>().onClick.AddListener(OnClickedPurchase);
            }

            foreach (var kvp in _treasureItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;
            }
        }

        public void RefreshUIProducts()
        {
            _treasureItemToUIMap = new Dictionary<Treasure, PawnShopUIItem>();
            List<Treasure> orderedList = _availableItems.OrderBy(o => o.treasureName.Value).ToList();

            foreach (var item in orderedList)
            {
                var itemGO = Instantiate(_itemUIPrefab, _itemUIRoot);
                var itemUI = itemGO.GetComponent<PawnShopUIItem>();
                itemUI.Bind(item, OnItemSelected);
                _treasureItemToUIMap[item] = itemUI;
            }

            RefreshUICommon();
        }

        public void OnClickedPurchase()
        {
            GameManager.Instance.GainFunds(_selectedItem.value);
            RefreshUICommon();
        }

        private void OnItemSelected(Treasure newlySelectedItem)
        {
            _selectedItem = newlySelectedItem;

            foreach (var kvp in _treasureItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;

                itemUI.SetIsSelected(item == _selectedItem);
            }

            RefreshUICommon();
        }
    }
}

using General;
using SaveSystem;
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
        [SerializeField] Button _sellButton;

        Dictionary<Treasure, PawnShopUIItem> _treasureItemToUIMap;
        Treasure _selectedItem;
        List<Treasure> _availableItems;

        private void Start()
        {
            GetPlayerTreasure();
            RefreshUIProducts();
        }

        private void GetPlayerTreasure()
        {
            _availableItems = new List<Treasure>();

            if (SaveManager.Instance.PlayerData.obtainedTreasures.Length != 0)
            {
                foreach (var item in SaveManager.Instance.PlayerData.obtainedTreasures)
                {
                    _availableItems.Add(BodyPartsCollection.Instance.ReturnTreasure(item.id));
                }
            }
        }

        public void RefreshUICommon()
        {
            _availableFundsText.text = "";
            _availableFundsText.text = $"{(GameManager.Instance.GetCurrentFunds() / 100f):0.00}";
            _sellButton.GetComponent<Button>().onClick.RemoveAllListeners();

            if (_selectedItem != null && BodyPartsCollection.Instance.ReturnPlayerTreasure(_selectedItem.id).amount > 0)
            {
                _sellButton.GetComponent<Button>().onClick.AddListener(OnClickedPurchase);
            }

            foreach (var kvp in _treasureItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;
                itemUI.SetCanSell(BodyPartsCollection.Instance.ReturnPlayerTreasure(item.id).amount > 0);
            }
        }

        public void RefreshUIProducts()
        {
            _treasureItemToUIMap = new Dictionary<Treasure, PawnShopUIItem>();
            List<Treasure> orderedList = _availableItems.OrderBy(o => o.treasureName.Value).ToList();

            for (int i = 0; i < _availableItems.Count; i++)
            {
                var itemGO = Instantiate(_itemUIPrefab, _itemUIRoot);
                var itemUI = itemGO.GetComponent<PawnShopUIItem>();
                itemUI.Bind(_availableItems[i], OnItemSelected);
                _treasureItemToUIMap[_availableItems[i]] = itemUI;
            }

            RefreshUICommon();
        }

        public void OnClickedPurchase()
        {
            GameManager.Instance.GainFunds(_selectedItem.value);
            GameManager.Instance.SpendTreasure(_selectedItem.id, 1);
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

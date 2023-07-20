using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Buildings.ShopSystem
{
    public class SupermarketUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _itemName;
        [SerializeField] TextMeshProUGUI _itemDescription;
        [SerializeField] TextMeshProUGUI _itemPrice;
        [SerializeField] Image _icon;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        [SerializeField] Image _backgroundPanel;

        private FoodItem _foodItem;
        private UnityAction<FoodItem> OnSelectedFn;

        public void Bind(FoodItem item, UnityAction<FoodItem> onSelectedFn)
        {
            _foodItem = item;
            _itemName.text = _foodItem.foodName.Value;
            _itemDescription.text = _foodItem.description.Value;
            _itemPrice.text = $"${_foodItem.cost / 100f:0.00}";
            _icon.sprite = _foodItem.icon;
            OnSelectedFn = onSelectedFn;
            SetIsSelected(false);
        }

        public void SetIsSelected(bool isSelected)
        {
            _backgroundPanel.color = isSelected ? _selectedColor : _defaultColor;
        }

        public void OnClicked()
        {
            OnSelectedFn.Invoke(_foodItem);
        }

        public void SetCanAfford(bool canAfford)
        {
            _itemPrice.color = canAfford ? Color.black : Color.red;
        }
    }
}

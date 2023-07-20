using Buildings.ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Buildings.Apartments.Rooms
{
    public class FoodUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _itemName;
        [SerializeField] TextMeshProUGUI _itemDescription;
        [SerializeField] TextMeshProUGUI _itemAmount;
        [SerializeField] Image _icon;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        [SerializeField] Image _backgroundPanel;

        private FoodItem _foodItem;
        private UnityAction<FoodItem> OnSelectedFn;

        public void Bind(FoodItem item, int amount, UnityAction<FoodItem> onSelectedFn)
        {
            _foodItem = item;
            _itemName.text = _foodItem.foodName.Value;
            _itemDescription.text = _foodItem.description.Value;
            _itemAmount.text = amount.ToString();
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
    }
}

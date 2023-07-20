using Buildings.ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Buildings.Apartments.Rooms
{
    public class FurnitureUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _itemName;
        [SerializeField] TextMeshProUGUI _itemDescription;
        [SerializeField] TextMeshProUGUI _itemQuantity;
        [SerializeField] Image _icon;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        [SerializeField] Image _backgroundPanel;

        private FurnitureItem _furnitureItem;
        private UnityAction<FurnitureItem> OnSelectedFn;

        public void Bind(FurnitureItem item, int amount, UnityAction<FurnitureItem> onSelectedFn)
        {
            _furnitureItem = item;
            _itemName.text = _furnitureItem.furnitureName.Value;
            _itemDescription.text = _furnitureItem.description.Value;
            _itemQuantity.text = amount.ToString();
            _icon.sprite = _furnitureItem.icon;
            OnSelectedFn = onSelectedFn;
            SetIsSelected(false);
        }

        public void SetIsSelected(bool isSelected)
        {
            _backgroundPanel.color = isSelected ? _selectedColor : _defaultColor;
        }

        public void OnClicked()
        {
            OnSelectedFn.Invoke(_furnitureItem);
        }
    }
}
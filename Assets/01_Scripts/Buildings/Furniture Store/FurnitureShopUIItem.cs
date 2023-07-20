using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Buildings.ShopSystem
{
    public class FurnitureShopUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _itemName;
        [SerializeField] TextMeshProUGUI _itemDescription;
        [SerializeField] TextMeshProUGUI _itemPrice;
        [SerializeField] Image _icon;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        [SerializeField] Image _backgroundPanel;

        private FurnitureItem _furnitureItem;
        private UnityAction<FurnitureItem> OnSelectedFn;

        public void Bind(FurnitureItem item, UnityAction<FurnitureItem> onSelectedFn)
        {
            _furnitureItem = item;
            _itemName.text = _furnitureItem.furnitureName.Value;
            _itemDescription.text = _furnitureItem.description.Value;
            _itemPrice.text = $"${_furnitureItem.cost / 100f:0.00}";
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

        public void SetCanAfford(bool canAfford)
        {
            _itemPrice.color = canAfford ? Color.black : Color.red;
        }
    }
}

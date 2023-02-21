using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShopSystem
{
    public class PawnShopUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _treasureName;
        [SerializeField] TextMeshProUGUI _treasureDescription;
        [SerializeField] TextMeshProUGUI _treasurePrice;
        [SerializeField] Image _icon;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        [SerializeField] Image _backgroundPanel;

        private Treasure _treasureItem;
        private UnityAction<Treasure> OnSelectedFn;

        public void Bind(Treasure item, UnityAction<Treasure> onSelectedFn)
        {
            _treasureItem = item;
            _treasureName.text = _treasureItem.treasureName.Value;
            _treasureDescription.text = _treasureItem.description.Value;
            _treasurePrice.text = $"${_treasureItem.value / 100f:0.00}";
            _icon.sprite = _treasureItem.icon;
            OnSelectedFn = onSelectedFn;
            SetIsSelected(false);
        }

        public void SetIsSelected(bool isSelected)
        {
            _backgroundPanel.color = isSelected ? _selectedColor : _defaultColor;
        }

        public void OnClicked()
        {
            OnSelectedFn.Invoke(_treasureItem);
        }
    }
}

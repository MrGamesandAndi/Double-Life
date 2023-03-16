using SaveSystem;
using ShopSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Apartments
{
    public class TreasureUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _itemName;
        [SerializeField] TextMeshProUGUI _itemDescription;
        [SerializeField] TextMeshProUGUI _itemQuantity;
        [SerializeField] Image _icon;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        [SerializeField] Image _backgroundPanel;

        private Treasure _treasure;
        private UnityAction<Treasure> OnSelectedFn;

        public void Bind(Treasure item, int amount, UnityAction<Treasure> onSelectedFn)
        {
            _treasure = item;
            _itemName.text = _treasure.treasureName.Value;
            _itemDescription.text = _treasure.description.Value;
            _itemQuantity.text = amount.ToString();
            _icon.sprite = _treasure.icon;
            OnSelectedFn = onSelectedFn;
            SetIsSelected(false);
        }

        public void SetIsSelected(bool isSelected)
        {
            _backgroundPanel.color = isSelected ? _selectedColor : _defaultColor;
        }

        public void OnClicked()
        {
            OnSelectedFn.Invoke(_treasure);
        }
    }
}

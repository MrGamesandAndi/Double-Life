using SaveSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TownHall
{
    public class DoubleUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _fullName;
        [SerializeField] List<int> _traits;
        [SerializeField] int _gender;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        [SerializeField] Image _backgroundPanel;

        CharacterData _double;
        UnityAction<CharacterData> OnSelectedFn;

        public void Bind(CharacterData item, UnityAction<CharacterData> onSelectedFn)
        {
            _double = item;
            _fullName.text = _double.Name + " " + _double.LastName;
            _traits = _double.Traits;
            _gender = _double.Gender;
            OnSelectedFn = onSelectedFn;
            SetIsSelected(false);
        }

        public void SetIsSelected(bool isSelected)
        {
            _backgroundPanel.color = isSelected ? _selectedColor : _defaultColor;
        }

        public void OnClicked()
        {
            OnSelectedFn.Invoke(_double);
        }
    }
}

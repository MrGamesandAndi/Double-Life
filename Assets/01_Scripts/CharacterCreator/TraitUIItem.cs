using TMPro;
using TraitSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterCreator
{
    public class TraitUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _traitName;
        [SerializeField] TextMeshProUGUI _traitDescription;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        [SerializeField] Color _unavailableColor;
        [SerializeField] Image _backgroundPanel;

        private Trait _traitItem;
        private UnityAction<Trait> OnSelectedFn;

        public Trait TraitItem { get => _traitItem; set => _traitItem = value; }

        public void Bind(Trait item, UnityAction<Trait> onSelectedFn)
        {
            _traitItem = item;
            _traitName.text = _traitItem.displayName.Value;
            _traitDescription.text = _traitItem.description.Value;
            OnSelectedFn = onSelectedFn;
            SetIsSelected(false);
        }

        public void SetIsSelected(bool isSelected)
        {
            _backgroundPanel.color = isSelected ? _selectedColor : _defaultColor;
        }

        public void OnClicked()
        {
            OnSelectedFn.Invoke(_traitItem);
        }
    }
}

using General;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TraitSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterCreator
{
    public class TraitsUI : MonoBehaviour
    {
        [SerializeField] Transform _itemUIRoot;
        [SerializeField] GameObject _itemUIPrefab;
        [SerializeField] List<Trait> _availableTraits = new List<Trait>();
        [SerializeField] List<GameObject> _traitSlots;
        [SerializeField] Button _giveButton;
        [SerializeField] Button _removeButton;

        Dictionary<Trait, TraitUIItem> _traitItemToUIMap;
        Trait _selectedTrait;

        private void Start()
        {
            foreach (var trait in BodyPartsCollection.Instance.traits)
            {
                _availableTraits.Add(trait);
            }

            _removeButton.GetComponent<Button>().onClick.AddListener(OnClickedRemove);
            RefreshUITraits();
        }

        public void RefreshUICommon()
        {
            _giveButton.GetComponent<Button>().onClick.RemoveAllListeners();

            if (_selectedTrait != null)
            {
                _giveButton.GetComponent<Button>().onClick.AddListener(OnClickedGive);

            }

            foreach (var kvp in _traitItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;
            }
        }

        public void RefreshUITraits()
        {
            _traitItemToUIMap = new Dictionary<Trait, TraitUIItem>();
            List<Trait> orderedList = _availableTraits.OrderBy(o => o.displayName.Value).ToList();

            foreach (var item in orderedList)
            {
                var itemGO = Instantiate(_itemUIPrefab, _itemUIRoot);
                var itemUI = itemGO.GetComponent<TraitUIItem>();
                itemUI.Bind(item, OnItemSelected);
                _traitItemToUIMap[item] = itemUI;
            }

            RefreshUICommon();
        }

        public void OnClickedGive()
        {
            for (int i = 0; i < _traitSlots.Count; i++)
            {
                if (_traitSlots[i].GetComponentInChildren<TextMeshProUGUI>().text == string.Empty)
                {
                    _traitSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = _selectedTrait.displayName.Value;
                    HumanController.Instance.characterData.Traits[i] = _selectedTrait.id;
                    _selectedTrait = null;
                    RefreshUICommon();
                    break;
                }
                else
                {
                    if (_traitSlots[i].GetComponentInChildren<TextMeshProUGUI>().text == _selectedTrait.displayName.Value)
                    {
                        _selectedTrait = null;
                        break;
                    }
                }
            }

            RefreshUICommon();
        }

        public void OnClickedRemove()
        {
            for (int i = 0; i < _traitSlots.Count; i++)
            {
                if (_traitSlots[i].GetComponentInChildren<TextMeshProUGUI>().text != string.Empty)
                {
                    _traitSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
                    HumanController.Instance.characterData.Traits[i] = 0;
                    break;
                }
            }
        }

        private void OnItemSelected(Trait newlySelectedTrait)
        {
            _selectedTrait = newlySelectedTrait;

            foreach (var kvp in _traitItemToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;

                itemUI.SetIsSelected(item == _selectedTrait);
            }

            RefreshUICommon();
        }

        public void SaveSelectedTraits()
        {

        }
    }
}

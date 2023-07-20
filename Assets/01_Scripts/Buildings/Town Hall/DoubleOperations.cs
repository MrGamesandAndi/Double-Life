using General;
using Localisation;
using Population;
using Relationships;
using SaveSystem;
using SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings.TownHall
{
    public class DoubleOperations : MonoBehaviour
    {
        [SerializeField] GameObject _doubleButtonPrefab;
        [SerializeField] Transform _doubleButtonUIRoot;
        [SerializeField] List<CharacterData> _doubleList;
        [SerializeField] Button _editButton;
        [SerializeField] Button _deleteButton;
        [SerializeField] LocalisedString _deletePrompt;

        private Dictionary<CharacterData, DoubleUIItem> _doubleToUIMap;
        private CharacterData _selectedDouble;

        private void Start()
        {
            _doubleList = PopulationManager.Instance.DoublesList;
            RefreshUIDoubles();
        }

        public void RefreshUICommon()
        {
            _editButton.GetComponent<Button>().onClick.RemoveAllListeners();
            _deleteButton.GetComponent<Button>().onClick.RemoveAllListeners();

            if (_selectedDouble != null)
            {
                _editButton.interactable = true;
                _editButton.GetComponent<Button>().onClick.AddListener(OnClickedEdit);
                _deleteButton.interactable = true;
                _deleteButton.GetComponent<Button>().onClick.AddListener(OnClickedDelete);

            }
            else
            {
                _editButton.interactable = false;
                _deleteButton.interactable = false;
            }

            foreach (var kvp in _doubleToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;
            }
        }

        public void RefreshUIDoubles()
        {
            _doubleToUIMap = new Dictionary<CharacterData, DoubleUIItem>();
            List<CharacterData> orderedList = _doubleList.OrderBy(o => o.Name).ToList();

            foreach (var item in orderedList)
            {
                var itemGO = Instantiate(_doubleButtonPrefab, _doubleButtonUIRoot);
                var itemUI = itemGO.GetComponent<DoubleUIItem>();
                itemUI.Bind(item, OnItemSelected);
                _doubleToUIMap[item] = itemUI;
            }

            RefreshUICommon();
        }

        public void OnClickedEdit()
        {
            PlayerPrefs.SetInt("CC_State", 1);
            _selectedDouble = GameManager.Instance.currentLoadedDouble;
            ScenesManager.Instance.LoadScene(Scenes.Character_Creator, Scenes.Town_Hall);
        }

        public void OnClickedDelete()
        {
            if(_selectedDouble.RelationshipCode != "cc_rel_7")
            {
                ModalWindow.Instance.ShowQuestion(_deletePrompt.Value, () =>
                {
                    RelationshipSystem.Instance.DeleteRelationship(_selectedDouble.Id);
                    GenerateAI.Instance.RemoveIndividualAI(_selectedDouble);
                    PopulationManager.Instance.RemoveDouble(_selectedDouble.Id);
                    RefreshUIDoubles();
                    ScenesManager.Instance.LoadScene(Scenes.City, Scenes.Town_Hall);
                }, () => { });
            }
        }

        private void OnItemSelected(CharacterData newlySelectedItem)
        {
            _selectedDouble = newlySelectedItem;
            GameManager.Instance.currentLoadedDouble = _selectedDouble;

            foreach (var kvp in _doubleToUIMap)
            {
                var item = kvp.Key;
                var itemUI = kvp.Value;

                itemUI.SetIsSelected(item == _selectedDouble);
            }

            RefreshUICommon();
        }
    }
}

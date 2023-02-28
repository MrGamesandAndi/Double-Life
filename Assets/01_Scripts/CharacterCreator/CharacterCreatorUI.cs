using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterCreator
{
    public class CharacterCreatorUI : MonoBehaviour
    {
        [SerializeField] Transform _itemUIRoot;
        [SerializeField] GameObject _itemUIPrefab;
        [SerializeField] List<BaseScriptableObject> _availableBodyParts;

        private void Start()
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (var item in _availableBodyParts)
            {
                Instantiate(CreateIcon(item,_itemUIPrefab), _itemUIRoot);
            }
        }

        private GameObject CreateIcon(BaseScriptableObject scriptableObject, GameObject prefab)
        {
            prefab.name = scriptableObject.name;
            prefab.GetComponentInChildren<Image>().sprite = scriptableObject.Icon;
            prefab.GetComponentInChildren<Image>().color = scriptableObject.Color;
            prefab.GetComponentInChildren<DetailsButtonController>().objectInfo = scriptableObject;
            return prefab;
        }
    }
}
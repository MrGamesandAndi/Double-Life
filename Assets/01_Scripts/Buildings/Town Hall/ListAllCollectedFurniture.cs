using Buildings.ShopSystem;
using SaveSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Buildings.TownHall
{
    public class ListAllCollectedFurniture : MonoBehaviour
    {
        [SerializeField] GameObject _furnitureButtonPrefab;
        [SerializeField] Transform _furnitureUIRoot;
        [SerializeField] List<FurnitureItem> _furnitureList;

        List<PurchasedItem> _purchasedFurnitureList;

        private void Start()
        {
            _purchasedFurnitureList = SaveManager.Instance.FurnitureData;
            RefreshUIFurniture();
        }

        public void RefreshUIFurniture()
        {
            List<PurchasedItem> orderedList = _purchasedFurnitureList.OrderBy(o => o.itemName.Value).ToList();
            List<FurnitureItem> furniture = _furnitureList.OrderBy(o => o.furnitureName.Value).ToList();

            foreach (var item in furniture)
            {
                for (int i = 0; i < _purchasedFurnitureList.Count; i++)
                {
                    if (item.furnitureName.Value == _purchasedFurnitureList[i].itemName.Value)
                    {
                        var itemGO = Instantiate(_furnitureButtonPrefab, _furnitureUIRoot);
                        var itemUI = itemGO.GetComponent<CollectedFurnitureUIItem>();
                        itemUI.Bind(item);
                        break;
                    }
                }
            }
        }
    }
}

using SaveSystem;
using ShopSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TownHall
{
    public class ListAllCollectedFurniture : MonoBehaviour
    {
        [SerializeField] GameObject _furnitureButtonPrefab;
        [SerializeField] Transform _furnitureUIRoot;
        [SerializeField] List<PurchasedFurniture> _purchasedFurnitureList;
        [SerializeField] List<FurnitureItem> _furnitureList;

        private void Start()
        {
            _purchasedFurnitureList = SaveManager.Instance.FurnitureData;
            RefreshUIFood();
        }

        public void RefreshUIFood()
        {
            List<PurchasedFurniture> orderedList = _purchasedFurnitureList.OrderBy(o => o.furnitureName.Value).ToList();
            List<FurnitureItem> furniture = _furnitureList.OrderBy(o => o.furnitureName.Value).ToList();

            foreach (var item in furniture)
            {
                for (int i = 0; i < _purchasedFurnitureList.Count; i++)
                {
                    if (item.furnitureName.Value == _purchasedFurnitureList[i].furnitureName.Value)
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

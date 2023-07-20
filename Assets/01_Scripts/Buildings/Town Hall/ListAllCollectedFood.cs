using Buildings.ShopSystem;
using SaveSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Buildings.TownHall
{
    public class ListAllCollectedFood : MonoBehaviour
    {
        [SerializeField] GameObject _foodButtonPrefab;
        [SerializeField] Transform _foodUIRoot;
        [SerializeField] List<PurchasedItem> _purchasedFoodList;
        [SerializeField] List<FoodItem> _foodList;

        private void Start()
        {
            _purchasedFoodList = SaveManager.Instance.FoodData;
            RefreshUIFood();
        }

        public void RefreshUIFood()
        {
            List<PurchasedItem> orderedList = _purchasedFoodList.OrderBy(o => o.itemName.Value).ToList();
            List<FoodItem> foods = _foodList.OrderBy(o => o.foodName.Value).ToList();

            foreach (var item in foods)
            {
                for (int i = 0; i < _purchasedFoodList.Count; i++)
                {
                    if (item.foodName.Value == _purchasedFoodList[i].itemName.Value)
                    {
                        var itemGO = Instantiate(_foodButtonPrefab, _foodUIRoot);
                        var itemUI = itemGO.GetComponent<CollectedFoodUIItem>();
                        itemUI.Bind(item);
                        break;
                    }
                }
            }
        }
    }
}

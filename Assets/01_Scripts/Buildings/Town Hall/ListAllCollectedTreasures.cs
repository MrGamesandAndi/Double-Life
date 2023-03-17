using General;
using SaveSystem;
using ShopSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TownHall
{
    public class ListAllCollectedTreasures : MonoBehaviour
    {
        [SerializeField] GameObject _treasureButtonPrefab;
        [SerializeField] Transform _treasureUIRoot;

        List<Treasures> _obtainedTreasuresList;
        List<Treasure> _treasuresList;

        private void Start()
        {
            _obtainedTreasuresList = new List<Treasures>();
            _treasuresList = new List<Treasure>();
            _obtainedTreasuresList = SaveManager.Instance.PlayerData.obtainedTreasures.ToList();

            foreach (var item in _obtainedTreasuresList)
            {
                if(item.amount > 0)
                {
                    _treasuresList.Add(BodyPartsCollection.Instance.ReturnTreasure(item.id));
                }
            }

            if(_treasuresList.Count != 0)
            {
                RefreshUIFurniture();
            }
        }

        public void RefreshUIFurniture()
        {
            List<Treasure> treasures = _treasuresList.OrderBy(o => o.treasureName.Value).ToList();

            foreach (var item in treasures)
            {
                var itemGO = Instantiate(_treasureButtonPrefab, _treasureUIRoot);
                var itemUI = itemGO.GetComponent<CollectedTreasureUIItem>();
                itemUI.Bind(item);
            }
        }
    }
}

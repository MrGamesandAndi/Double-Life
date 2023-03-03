using Localisation;
using UnityEngine;

namespace ShopSystem
{
    [CreateAssetMenu(menuName = "Shop/PawnStore/Treasure", fileName = "TreasureItem_")]
    public class Treasure : ScriptableObject
    {
        public int id;
        public LocalisedString treasureName;
        public LocalisedString description;
        public Sprite icon;
        public int value;
        public TreasureRarity rarity;
    }
}

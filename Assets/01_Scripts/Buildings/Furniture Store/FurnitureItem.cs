using Localisation;
using UnityEngine;

namespace Buildings.ShopSystem
{
    [CreateAssetMenu(menuName = "Shop/FurnitureStore/Furniture", fileName = "FurnitureItem_")]
    public class FurnitureItem : ScriptableObject
    {
        public LocalisedString furnitureName;
        public LocalisedString description;
        public Sprite icon;
        public int cost;
        public GameObject prefab;
        public Vector2Int size = Vector2Int.one;
        public int id;     
    }
}

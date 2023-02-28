using Localisation;
using UnityEngine;

namespace ShopSystem
{
    [CreateAssetMenu(menuName = "Shop/Supermarket/Food", fileName = "FoodItem_")]
    public class FoodItem : ScriptableObject
    {
        public FoodItemCategory category;
        public LocalisedString foodName;
        public LocalisedString description;
        public Sprite icon;
        public int cost;
        public int experienceReward = 50;
    }
}

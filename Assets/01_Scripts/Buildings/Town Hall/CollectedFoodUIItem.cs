using Buildings.ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings.TownHall
{
    public class CollectedFoodUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _name;
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _description;

        public void Bind(FoodItem originalFoodData)
        {
            _name.text = originalFoodData.foodName.Value;
            _icon.sprite = originalFoodData.icon;
            _description.text = originalFoodData.description.Value;
        }
    }
}

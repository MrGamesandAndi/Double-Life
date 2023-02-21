using ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownHall
{
    public class CollectedFurnitureUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _name;
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _description;

        public void Bind(FurnitureItem furnitureData)
        {
            _name.text = furnitureData.furnitureName.Value;
            _icon.sprite = furnitureData.icon;
            _description.text = furnitureData.description.Value;
        }
    }
}
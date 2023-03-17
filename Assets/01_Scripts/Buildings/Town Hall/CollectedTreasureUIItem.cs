using ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownHall
{
    public class CollectedTreasureUIItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _name;
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _description;

        public void Bind(Treasure treasureData)
        {
            _name.text = treasureData.treasureName.Value;
            _icon.sprite = treasureData.icon;
            _description.text = treasureData.description.Value;
        }
    }
}

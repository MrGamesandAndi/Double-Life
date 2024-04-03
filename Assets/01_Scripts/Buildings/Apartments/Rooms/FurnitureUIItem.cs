using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings.Apartments.Rooms
{
    public class FurnitureUIItem : MonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] TextMeshProUGUI _quantity;

        GameObject _prefab;
        int _id;

        public Image Image { get => _image; set => _image = value; }
        public TextMeshProUGUI Quantity { get => _quantity; set => _quantity = value; }
        public GameObject Prefab { get => _prefab; set => _prefab = value; }
        public int Id { get => _id; set => _id = value; }

        public void OnClick()
        {
            PlacementSystem placementSystem = FindObjectOfType<PlacementSystem>();
            placementSystem.StartPlacement(Id);
        }
    }
}
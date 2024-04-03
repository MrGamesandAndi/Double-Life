using General;
using SaveSystem;
using UnityEngine;

namespace Buildings.Apartments.Rooms
{
    public class FurnitureUI : MonoBehaviour
    {
        [SerializeField] GameObject _furnitureUIPrefab;

        private void Start()
        {
            for (int i = 0; i < SaveManager.Instance.FurnitureData.Count; i++)
            {
                for (int j = 0; j < BodyPartsCollection.Instance.furniture.Count; j++)
                {
                    if (SaveManager.Instance.FurnitureData[i].itemName.Value == BodyPartsCollection.Instance.furniture[j].furnitureName.Value)
                    {
                        GameObject newButton = Instantiate(_furnitureUIPrefab, transform);
                        newButton.GetComponent<FurnitureUIItem>().Image.sprite = BodyPartsCollection.Instance.furniture[j].icon;
                        newButton.GetComponent<FurnitureUIItem>().Quantity.text = SaveManager.Instance.FurnitureData[i].amount.ToString();
                        newButton.GetComponent<FurnitureUIItem>().Prefab = BodyPartsCollection.Instance.furniture[j].prefab;
                        newButton.GetComponent<FurnitureUIItem>().Id = BodyPartsCollection.Instance.furniture[j].id;
                    }
                }
            }
        }
    }
}

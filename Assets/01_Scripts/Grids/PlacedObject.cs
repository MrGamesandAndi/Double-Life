using ShopSystem;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class PlacedObject : MonoBehaviour
    {
        public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, Dir dir, FurnitureItem placedObjectTypeSO)
        {
            Transform placedObjectTransform = Instantiate(placedObjectTypeSO.model, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));

            PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
            placedObject.Setup(placedObjectTypeSO, origin, dir);
            RoomManager.Instance.ShowTabs();
            return placedObject;
        }

        private FurnitureItem placedObjectTypeSO;
        private Vector2Int origin;
        private Dir dir;

        private void Setup(FurnitureItem placedObjectTypeSO, Vector2Int origin, Dir dir)
        {
            this.placedObjectTypeSO = placedObjectTypeSO;
            this.origin = origin;
            this.dir = dir;
        }

        public List<Vector2Int> GetGridPositionList()
        {
            return placedObjectTypeSO.GetGridPositionList(origin, dir);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public override string ToString()
        {
            return placedObjectTypeSO.furnitureName.Value;
        }
    }
}

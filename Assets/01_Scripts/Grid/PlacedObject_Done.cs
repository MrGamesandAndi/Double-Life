using ShopSystem;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject_Done : MonoBehaviour {

    public static PlacedObject_Done Create(Vector3 worldPosition, Vector2Int origin, FurnitureItem.Dir dir, FurnitureItem placedObjectTypeSO, Transform parent) {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0), parent);

        PlacedObject_Done placedObject = placedObjectTransform.GetComponent<PlacedObject_Done>();
        placedObject.Setup(placedObjectTypeSO, origin, dir);

        return placedObject;
    }

    public static PlacedObject_Done Create(Vector3 position,FurnitureItem placedObjectTypeSO, Transform parent)
    {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, position, Quaternion.Euler(0, 0, 0), parent);
        PlacedObject_Done placedObject = placedObjectTransform.GetComponent<PlacedObject_Done>();
        placedObject.Setup(placedObjectTypeSO, Vector2Int.zero, FurnitureItem.Dir.Down);
        return placedObject;
    }




    private FurnitureItem placedObjectTypeSO;
    private Vector2Int origin;
    private FurnitureItem.Dir dir;

    public FurnitureItem PlacedObjectTypeSO { get => placedObjectTypeSO; set => placedObjectTypeSO = value; }

    private void Setup(FurnitureItem placedObjectTypeSO, Vector2Int origin, FurnitureItem.Dir dir) {
        this.placedObjectTypeSO = placedObjectTypeSO;
        this.origin = origin;
        this.dir = dir;
    }

    public List<Vector2Int> GetGridPositionList() {
        return placedObjectTypeSO.GetGridPositionList(origin, dir);
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public override string ToString() {
        return placedObjectTypeSO.name;
    }

}

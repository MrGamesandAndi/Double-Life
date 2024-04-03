using General;
using SaveSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    List<GameObject> _placedGameObjects = new List<GameObject>();

    public int PlaceObject(int index, GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab, transform);
        newObject.transform.position = position;
        _placedGameObjects.Add(newObject);
        GameManager.Instance.currentLoadedDouble.PurchasedFurniture.Add(new CharacterData.FurnitureData(index, position));
        return _placedGameObjects.Count - 1;
    }

    public void RemoveObject(int gameObjectIndex)
    {
        if (_placedGameObjects.Count <= gameObjectIndex || _placedGameObjects[gameObjectIndex] == null)
        {
            return;
        }

        Destroy(_placedGameObjects[gameObjectIndex]);
        _placedGameObjects[gameObjectIndex] = null;
    }

    internal void LoadObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab, transform);
        newObject.transform.position = position;
        _placedGameObjects.Add(newObject);
    }
}

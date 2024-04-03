using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> _placedObjects = new Dictionary<Vector3Int, PlacementData>();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int id, int placedObjectsIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, id, placedObjectsIndex);

        foreach (var pos in positionToOccupy)
        {
            if (_placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell position:{pos}");
            }

            _placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new List<Vector3Int>();

        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }

        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);

        foreach (var pos in positionToOccupy)
        {
            if (_placedObjects.ContainsKey(pos))
            {
                return false;
            }
        }

        return true;
    }

    public int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if (!_placedObjects.ContainsKey(gridPosition))
        {
            return -1;
        }

        return _placedObjects[gridPosition].PlacedObjectsIndex;
    }

    public void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in _placedObjects[gridPosition].occupiedPositions)
        {
            _placedObjects.Remove(pos);
        }
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;

    public int ID { get; private set; }
    public int PlacedObjectsIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectsIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectsIndex = placedObjectsIndex;
    }
}

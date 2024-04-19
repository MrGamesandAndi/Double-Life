using General;
using UnityEngine;

public class RemovingState : IBuildingState
{
    int _gameObjectIndex = -1;
    Grid _grid;
    PreviewSystem _previewSystem;
    GridData _floorData;
    GridData _furnitureData;
    ObjectPlacer _objectPlacer;

    public RemovingState(Grid grid, PreviewSystem previewSystem, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer)
    {
        _grid = grid;
        _previewSystem = previewSystem;
        _floorData = floorData;
        _furnitureData = furnitureData;
        _objectPlacer = objectPlacer;

        _previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        _previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;

        if (!_furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one))
        {
            selectedData = _furnitureData;
        }
        else if (!_floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one))
        {
            selectedData = _floorData;
        }

        if (selectedData == null)
        {

        }
        else
        {
            _gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);

            if (_gameObjectIndex == -1)
            {
                return;
            }

            GameManager.Instance.currentLoadedDouble.PurchasedFurniture.RemoveAt(_gameObjectIndex);
            selectedData.RemoveObjectAt(gridPosition);
            _objectPlacer.RemoveObject(_gameObjectIndex);
        }

        Vector3 cellPosition = _grid.CellToWorld(gridPosition);
        _previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition));
    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        return !(_furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one) && _floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one));
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        _previewSystem.UpdatePosition(_grid.CellToWorld(gridPosition), validity);
    }

    public void OnLoad(int id, Vector3Int gridPosition)
    {
        return;
    }
}

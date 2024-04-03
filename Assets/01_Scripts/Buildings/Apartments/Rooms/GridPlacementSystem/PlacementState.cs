using General;
using UnityEngine;

public class PlacementState : IBuildingState
{
    int _selectedObjectIndex = -1;
    int _id;
    Grid _grid;
    PreviewSystem _previewSystem;
    GridData _floorData;
    GridData _furnitureData;
    ObjectPlacer _objectPlacer;
    SoundFeedback _soundFeedback;

    public PlacementState(int id, Grid grid, PreviewSystem previewSystem, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer, SoundFeedback soundFeedback)
    {
        _id = id;
        _grid = grid;
        _previewSystem = previewSystem;
        _floorData = floorData;
        _furnitureData = furnitureData;
        _objectPlacer = objectPlacer;
        _soundFeedback = soundFeedback;
        _selectedObjectIndex = BodyPartsCollection.Instance.furniture.FindIndex(data => data.id == id);

        if (_selectedObjectIndex > -1)
        {
            _previewSystem.StartShowingPlacementPreview(BodyPartsCollection.Instance.furniture[_selectedObjectIndex].prefab, BodyPartsCollection.Instance.furniture[_selectedObjectIndex].size);
        }
        else
        {
            throw new System.Exception($"No object with ID {id} exists");
        }
    }

    public void EndState()
    {
        _previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, _selectedObjectIndex);

        if (!placementValidity)
        {
            _soundFeedback.PlaySound(SoundType.WrongPlacement);
            return;
        }

        _soundFeedback.PlaySound(SoundType.Place);
        int index = _objectPlacer.PlaceObject(_selectedObjectIndex, BodyPartsCollection.Instance.furniture[_selectedObjectIndex].prefab, _grid.CellToWorld(gridPosition));
        GridData selectedData = BodyPartsCollection.Instance.furniture[_selectedObjectIndex].id == 0 ? _floorData : _furnitureData;
        selectedData.AddObjectAt(gridPosition, BodyPartsCollection.Instance.furniture[_selectedObjectIndex].size, BodyPartsCollection.Instance.furniture[_selectedObjectIndex].id, index);
        _previewSystem.UpdatePosition(_grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = BodyPartsCollection.Instance.furniture[_selectedObjectIndex].id == 0 ? _floorData : _furnitureData;
        return selectedData.CanPlaceObjectAt(gridPosition, BodyPartsCollection.Instance.furniture[_selectedObjectIndex].size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, _selectedObjectIndex);
        _previewSystem.UpdatePosition(_grid.CellToWorld(gridPosition), placementValidity);
    }
}

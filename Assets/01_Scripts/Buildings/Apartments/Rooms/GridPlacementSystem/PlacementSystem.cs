using Buildings.ShopSystem;
using General;
using Needs;
using Population;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] InputManager _inputManager;
    [SerializeField] Grid _grid;
    [SerializeField] GameObject _gridVisualization;
    [SerializeField] PreviewSystem _preview;
    [SerializeField] ObjectPlacer _objectPlacer;
    [SerializeField] SoundFeedback _soundFeedback;

    IBuildingState _buildingState;
    GridData _floorData;
    GridData _furnitureData;
    Vector3Int _lastDetectedPosition = Vector3Int.zero;
    bool _hasPlaced = false;

    private void Start()
    {
        StopPlacement();
        _floorData = new GridData();
        _furnitureData = new GridData();

        foreach (var item in GameManager.Instance.currentLoadedDouble.PurchasedFurniture)
        {
            _buildingState = new PlacementState(item.id, _grid, _floorData, _furnitureData, _objectPlacer);
            _buildingState.OnLoad(item.id, _grid.WorldToCell(item.position));
            _buildingState = null;
        }
    }   

    private void Update()
    {
        if (_buildingState == null)
        {
            return;
        }

        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition=_grid.WorldToCell(mousePosition);

        if (_lastDetectedPosition != gridPosition)
        {
            _buildingState.UpdateState(gridPosition);
            _lastDetectedPosition = gridPosition;
        }
    }

    public void StartPlacement(int id)
    {
        StopPlacement();
        _gridVisualization.SetActive(true);
        _buildingState = new PlacementState(id, _grid, _preview, _floorData, _furnitureData, _objectPlacer, _soundFeedback);
        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        _gridVisualization.SetActive(true);
        _buildingState = new RemovingState(_grid, _preview, _floorData, _furnitureData, _objectPlacer);
        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (_inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        _buildingState.OnAction(gridPosition);
        _hasPlaced = true;
    }

    private void StopPlacement()
    {
        if (_buildingState == null)
        {
            return;
        }

        _gridVisualization.SetActive(false);
        _buildingState.EndState();
        _inputManager.OnClicked -= PlaceStructure;
        _inputManager.OnExit -= StopPlacement;
        _lastDetectedPosition = Vector3Int.zero;
        _buildingState = null;

        if (GameManager.Instance.currentLoadedDouble.CurrentState == NeedType.BuyFurniture && _hasPlaced)
        {
            Treasure gainedTreasure = BodyPartsCollection.Instance.ReturnRandomTreasure(TreasureRarity.Uncommon);
            GameManager.Instance.GainTreasure(gainedTreasure.id, 1);
            PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).NeedCompleted(NeedType.BuyFurniture);
        }

        _hasPlaced = false;
    }
}

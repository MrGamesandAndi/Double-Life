using System;
using System.Collections.Generic;
using UnityEngine;
using ShopSystem;
using SaveSystem;
using General;
using Needs;

public class GridBuildingSystem3D : MonoBehaviour {

    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridHeight = 10;
    [SerializeField] float cellSize = 10f;
    [SerializeField] Transform _furnitureParent;
    public static GridBuildingSystem3D Instance { get; private set; }

    public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;


    private GridXZ<GridObject> grid;
    public FurnitureItem placedObjectTypeSO;
    private FurnitureItem.Dir dir;

    private void Awake() {
        Instance = this;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-7f, 0, 0), (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    private void Start()
    {
        Load();
        gameObject.SetActive(false);
    }

    public void SetFurniture()
    {
        if (RoomManager.Instance.SelectedFurniture != null)
        {
            placedObjectTypeSO = RoomManager.Instance.SelectedFurniture;
        }
    }

    public class GridObject {

        private GridXZ<GridObject> grid;
        private int x;
        private int y;
        public PlacedObject_Done placedObject;

        public GridObject(GridXZ<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        public override string ToString() {
            return x + ", " + y + "\n" + placedObject;
        }

        public void SetPlacedObject(PlacedObject_Done placedObject) {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearPlacedObject() {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public PlacedObject_Done GetPlacedObject() {
            return placedObject;
        }

        public bool CanBuild() {
            return placedObject == null;
        }

    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && placedObjectTypeSO != null) {
            Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
            grid.GetXZ(mousePosition, out int x, out int z);

            Vector2Int placedObjectOrigin = new Vector2Int(x, z);
            placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);

            // Test Can Build
            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(placedObjectOrigin, dir);
            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList) {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                PlacedObject_Done placedObject = PlacedObject_Done.Create(placedObjectWorldPosition, placedObjectOrigin, dir, placedObjectTypeSO, _furnitureParent);

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    Save(placedObject, gridPosition);
                }
                
                OnObjectPlaced?.Invoke(this, EventArgs.Empty);
                RoomManager.Instance.ShowTabs();
                RoomManager.Instance.DisableGrid();
                RoomManager.Instance.ResetSelectedFurniture();

                if(GameManager.Instance.currentLoadedDouble.CurrentState == DoubleState.Buy)
                {
                    RoomManager.Instance.DialogueRunner.StartDialogue("Thanks");
                    Treasure gainedTreasure = BodyPartsCollection.Instance.ReturnRandomTreasure(TreasureRarity.Rare);
                    GameManager.Instance.GainTreasure(gainedTreasure.id, 1);
                    PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).NeedCompleted(NeedType.BuyFurniture);
                }
            }
        }

        RefreshSelectedObjectType();


        if (Input.GetMouseButtonDown(1)) {
            Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
            if (grid.GetGridObject(mousePosition) != null) {
                // Valid Grid Position
                PlacedObject_Done placedObject = grid.GetGridObject(mousePosition).GetPlacedObject();
                if (placedObject != null)
                {
                    // Demolish
                    Save(placedObject, Vector2Int.zero);
                    placedObject.DestroySelf();
                    List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                    }
                }
            }
        }
    }

    private void RefreshSelectedObjectType() {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }


    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        grid.GetXZ(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (placedObjectTypeSO != null) {
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        } else {
            return mousePosition;
        }
    }

    public Quaternion GetPlacedObjectRotation() {
        if (placedObjectTypeSO != null) {
            return Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
        } else {
            return Quaternion.identity;
        }
    }

    public FurnitureItem GetPlacedObjectTypeSO() {
        return placedObjectTypeSO;
    }

    public void Save(PlacedObject_Done placedObject, Vector2Int coordinates)
    {
        CharacterData currentDouble = PopulationManager.Instance.ReturnDouble(GameManager.Instance.currentLoadedDouble.Id);
        currentDouble.PurchasedFurniture.Add(new Vector3(placedObject.PlacedObjectTypeSO.id, coordinates.x, coordinates.y));
    }

    private void Load()
    {
        CharacterData currentDouble = PopulationManager.Instance.ReturnDouble(GameManager.Instance.currentLoadedDouble.Id);

        if (currentDouble.PurchasedFurniture.Count != 0)
        {
            foreach (var item in currentDouble.PurchasedFurniture)
            {
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition((int)item.y, (int)item.z);
                PlacedObject_Done placedObject = PlacedObject_Done.Create(placedObjectWorldPosition, BodyPartsCollection.Instance.GetFurniture((int)item.x), _furnitureParent);
                grid.GetGridObject((int)item.y, (int)item.z).SetPlacedObject(placedObject);
            }
        }
    }

}

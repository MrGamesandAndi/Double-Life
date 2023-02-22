using General;
using LevelingSystem;
using SaveSystem;
using ShopSystem;
using System;
using System.Collections.Generic;
using TraitSystem;
using UnityEngine;

namespace GridSystem
{
    public class GridBuildingSystem : MonoBehaviour
    {
        public static GridBuildingSystem Instance { get; private set; }

        public event EventHandler OnSelectedChanged;
        public event EventHandler OnObjectPlaced;
        public event EventHandler OnLoaded;
        public int gridWidth = 10;
        public int gridHeight = 10;
        public float cellSize = 10f;
        public Vector3 initialPos = new Vector3(0, 0, 0);


        public GridXZ<GridObject> grid;
        public FurnitureItem placedObjectTypeSO;
        private Dir dir;

        private void Awake()
        {
            Instance = this;
            grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, initialPos, (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));
        }

        private void Start()
        {
            Load();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && placedObjectTypeSO != null)
            {
                Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
                grid.GetXZ(mousePosition, out int x, out int z);

                Vector2Int placedObjectOrigin = new Vector2Int(x, z);
                placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);

                List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(placedObjectOrigin, dir);
                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                    {
                        canBuild = false;
                        break;
                    }
                }

                if (canBuild)
                {
                    Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                    Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                    PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, placedObjectOrigin, dir, placedObjectTypeSO);

                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }

                    OnObjectPlaced?.Invoke(this, EventArgs.Empty);
                    RoomManager.Instance.DisableGrid();
                    RoomManager.Instance.ShowTabs();

                    if (GameManager.Instance.currentState == DoubleState.Buy)
                    {
                        var name = GameManager.Instance.currentLoadedDouble.Name + GameManager.Instance.currentLoadedDouble.LastName;

                        for (int i = 0; i < GenerateAI.Instance.transform.childCount; i++)
                        {
                            var ai = GenerateAI.Instance.transform.GetChild(i);

                            if (ai.gameObject.name == name)
                            {
                                RoomManager.Instance.humanModel.GetComponent<XPTracker>().AddXP(50);
                            }
                        }
                    }

                    //DeselectObjectType();
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                dir = FurnitureItem.GetNextDir(dir);
            }

            placedObjectTypeSO = RoomManager.Instance.SelectedFurniture; 
            RefreshSelectedObjectType();

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
                if (grid.GetGridObject(mousePosition) != null)
                {
                    // Valid Grid Position
                    PlacedObject placedObject = grid.GetGridObject(mousePosition).GetPlacedObject();
                    if (placedObject != null)
                    {
                        // Demolish
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

        private void RefreshSelectedObjectType()
        {
            OnSelectedChanged?.Invoke(this, EventArgs.Empty);
        }

        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            grid.GetXZ(worldPosition, out int x, out int z);
            return new Vector2Int(x, z);
        }

        public Vector3 GetMouseWorldSnappedPosition()
        {
            Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
            grid.GetXZ(mousePosition, out int x, out int z);

            if (placedObjectTypeSO != null)
            {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
                return placedObjectWorldPosition;
            }
            else
            {
                return mousePosition;
            }
        }

        public Quaternion GetPlacedObjectRotation()
        {
            if (placedObjectTypeSO != null)
            {
                return Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
            }
            else
            {
                return Quaternion.identity;
            }
        }

        public FurnitureItem GetPlacedObjectTypeSO()
        {
            return placedObjectTypeSO;
        }

        public void SetGO(FurnitureItem go)
        {
            placedObjectTypeSO = go;
        }

        public void Save()
        {
            List<GridObject.SaveObject> gridObjectSaveObjectList = new List<GridObject.SaveObject>();

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    GridObject gridObject = grid.GetGridObject(x, y);
                    gridObjectSaveObjectList.Add(gridObject.Save());
                }
            }

            GameManager.Instance.currentLoadedDouble.PurchasedFurniture = gridObjectSaveObjectList;
        }

        public void Load()
        {
            List<GridObject.SaveObject> savedList = GameManager.Instance.currentLoadedDouble.PurchasedFurniture;

            foreach (GridObject.SaveObject gridObjectSaveObject in savedList)
            {
                GridObject gridObject = grid.GetGridObject(gridObjectSaveObject.x, gridObjectSaveObject.y);
                grid.SetGridObject(gridObjectSaveObject.x, gridObjectSaveObject.y, gridObject);
            }
        }
    }
}

using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private List<PlacedObjectSO> placedObjectSOList;
    private PlacedObjectSO placedObjectSO;
    private Grid<GridObject> grid;
    private PlacedObjectSO.Dir dir = PlacedObjectSO.Dir.Down;
    public static GridBuildingSystem Instance;

    private void Awake()
    {
        Instance = this;
        int width = 24;
        int height = 16;
        float cellSize = 1f;
        grid = new Grid<GridObject>(width, height, cellSize, new Vector3(-12f, -8f), (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));

        placedObjectSO = placedObjectSOList[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);

            List<Vector2Int> gridPositionList =  placedObjectSO.GetGridPositionList(new Vector2Int(x, y), dir);

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
                Vector2Int rotationOffset = placedObjectSO.GetRotarionOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + 
                    new Vector3(rotationOffset.x, rotationOffset.y, 0) * grid.GetCellSize();

                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, y), dir, placedObjectSO);

                foreach (Vector2Int gridPosition in gridPositionList)
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }
            else
                UtilsClass.CreateWorldTextPopup("Cannot build here!", UtilsClass.GetMouseWorldPosition());
        }

        if (Input.GetMouseButtonDown(1))
        {
            GridObject gridObject = grid.GetGridObject(UtilsClass.GetMouseWorldPosition());
            PlacedObject placedObject = gridObject.GetPlacedObject();

            if (placedObject != null)
            {
                placedObject.DestroySelf();

                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

                foreach (Vector2Int gridPosition in gridPositionList)
                    grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
            dir = PlacedObjectSO.GetNextDir(dir);

        if (Input.GetKeyDown(KeyCode.Alpha1)) placedObjectSO = placedObjectSOList[0];
        if (Input.GetKeyDown(KeyCode.Alpha2)) placedObjectSO = placedObjectSOList[1];
        if (Input.GetKeyDown(KeyCode.Alpha3)) placedObjectSO = placedObjectSOList[2];
        if (Input.GetKeyDown(KeyCode.Alpha4)) placedObjectSO = placedObjectSOList[3];
        if (Input.GetKeyDown(KeyCode.Alpha5)) placedObjectSO = placedObjectSOList[4];
    }

    public class GridObject
    {
        private Grid<GridObject> grid;
        private PlacedObject placedObject;
        private int x;
        private int y;

        public GridObject(Grid<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetPlacedObject(PlacedObject placedObject)
        {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, y);
        }

        public PlacedObject GetPlacedObject()
        {
            return this.placedObject;
        }

        public void ClearPlacedObject()
        {
            this.placedObject = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public bool CanBuild()
        {
            return placedObject == null;
        }
    }
}
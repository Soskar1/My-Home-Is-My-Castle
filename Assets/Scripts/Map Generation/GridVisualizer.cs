using System.Collections.Generic;
using UnityEngine;

namespace Map.Grid
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject _empty;
        [SerializeField] private GameObject _filed;

        [SerializeField] private Transform _parent;

        private Dictionary<Vector2, GameObject> _gridCells = new Dictionary<Vector2, GameObject>();

        public void VisualizeGrid(MapGrid grid)
        {
            int area = grid.Width * grid.Length;

            for (int index = 0; index < area; index++)
            {
                Vector2 position = grid.CalculateCoordinatesFromIndex(index) + new Vector2(0.5f, 0.5f);

                Cell cell = grid.GetCell(position);
                SpawnGridCell(position, cell.ObjectType);
            }
        }

        public void ClearGrid()
        {
            foreach (GameObject gridCell in _gridCells.Values)
                Destroy(gridCell);

            _gridCells.Clear();
        }

        public void ReplaceGridCell(MapGrid grid, Vector2 coordinates, CellObjectType cellType)
        {
            GameObject currentGridCell = ReturnGridCell(coordinates);
            Destroy(currentGridCell);
            _gridCells.Remove(coordinates);

            SpawnGridCell(coordinates, cellType);
            grid.SetCell(coordinates, cellType);
        }

        private void SpawnGridCell(Vector2 coordinates, CellObjectType cellType)
        {
            GameObject gridInstance = null;
            if (cellType != CellObjectType.Empty)
                gridInstance = Instantiate(_filed, coordinates, Quaternion.identity);
            else
                gridInstance = Instantiate(_empty, coordinates, Quaternion.identity);

            gridInstance.transform.parent = _parent;
            _gridCells.Add(coordinates, gridInstance);
        }

        private GameObject ReturnGridCell(Vector2 coordinates)
        {
            if (_gridCells.TryGetValue(coordinates, out GameObject gridCell))
                return gridCell;

            return null;
        }
    }
}

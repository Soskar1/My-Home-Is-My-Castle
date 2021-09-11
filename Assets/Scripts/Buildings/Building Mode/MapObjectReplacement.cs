using Map.Generation;
using Map.Grid;
using UnityEngine;

namespace Map
{
    public class MapObjectReplacement : MonoBehaviour
    {
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private GridVisualizer _gridVisualizer;
        [SerializeField] private MapVisualizer _mapVisualizer;

        public void Replace(GameObject entity, Vector2 coordinates, CellObjectType replaceTo)
        {
            Cell cell = _mapGenerator.grid.GetCell(coordinates);
            Vector2 cellPos = new Vector2(cell.X, cell.Y);

            _mapVisualizer.ReplaceIndicator(cellPos, entity);
            _gridVisualizer.ReplaceGridCell(_mapGenerator.grid, cellPos, replaceTo);
        }

        public bool CheckForEmptiness(Vector2 position)
        {
            Cell cell = _mapGenerator.grid.GetCell(position);

            if (cell.ObjectType != CellObjectType.Empty)
                return false;

            return true;
        }

        public Vector2 CheckForEmptiness(Vector2[] possiblePositions)
        {
            foreach (var position in possiblePositions)
                if (CheckForEmptiness(position))
                    return position;

            return Vector2.zero;
        }

        public bool CheckWithCellObjectType(CellObjectType cellObjectType, Vector2 position)
        {
            Cell cell = _mapGenerator.grid.GetCell(position);

            if (cell.ObjectType != cellObjectType)
                return false;

            return true;
        }

        public Vector2 CheckWithCellObjectType(CellObjectType cellObjectType, Vector2[] possiblePositions)
        {
            foreach (var position in possiblePositions)
                if (CheckWithCellObjectType(cellObjectType, position))
                    return position;

            return Vector2.zero;
        }

        public CellObjectType CheckForCellObjectType(Vector2 position)
        {
            Cell cell = _mapGenerator.grid.GetCell(position);
            return cell.ObjectType;
        }
    }
}

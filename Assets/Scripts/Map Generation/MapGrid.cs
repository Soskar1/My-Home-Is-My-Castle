using UnityEngine;

namespace Map.Grid
{
    public class MapGrid
    {
        private int _width;
        private int _length;
        private Cell[,] _cellGrid;

        public int Width { get => _width;}
        public int Length { get => _length;}

        public MapGrid(int width, int length)
        {
            _width = width;
            _length = length;
            CreateGrid();
        }

        private void CreateGrid()
        {
            _cellGrid = new Cell[_length, _width];

            for (int row = 0; row < _length; row++)
                for (int col = 0; col < _width; col++)
                    _cellGrid[row, col] = new Cell(col, row);
        }

        public void SetCell(int x, int y, CellObjectType objectType, bool isTaken = false)
        {
            _cellGrid[y, x].ObjectType = objectType;
            _cellGrid[y, x].IsTaken = isTaken;
        }

        public void SetCell(Vector2 coordinates, CellObjectType objectType, bool isTaken = false)
        {
            SetCell((int)coordinates.x, (int)coordinates.y, objectType, isTaken);
        }

        public bool IsCellTaken(int x, int y)
        { 
            return _cellGrid[x, y].IsTaken;
        }

        public bool IsCellTaken(Vector2 coordinates)
        {
            return _cellGrid[(int)coordinates.x, (int)coordinates.y].IsTaken;
        }

        public bool IsCellValid(Vector2 coordinates)
        {
            if (coordinates.x >= _width || coordinates.x < 0 ||
                coordinates.y >= _length || coordinates.y < 0)
                return false;

            return true;
        }

        public Cell GetCell(int x, int y)
        {
            Vector2 coordinates = new Vector2(x, y);
            if (!IsCellValid(coordinates))
                return null;

            return _cellGrid[y, x];
        }

        public Cell GetCell(Vector2 coordinates)
        {
            return GetCell((int)coordinates.x, (int)coordinates.y);
        }

        public int CalculateIndexFromCoordinates(int x, int y)
        {
            return x + y * _width;
        }

        public int CalculateIndexFromCoordinates(Vector2 coordinates)
        {
            return (int)coordinates.x + (int)coordinates.y * _width;
        }

        public Vector2 CalculateCoordinatesFromIndex(int randomIndex)
        {
            int x = randomIndex % _width;
            int y = randomIndex / _width;
            return new Vector2(x, y);
        }
    }
}

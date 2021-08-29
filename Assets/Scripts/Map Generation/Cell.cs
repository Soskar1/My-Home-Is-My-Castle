namespace Map.Grid
{
    public class Cell
    {
        private int _x;
        private int _y;
        private bool _isTaken;
        private CellObjectType _objectType;

        public int X { get => _x; }
        public int Y { get => _y; }
        public bool IsTaken { get => _isTaken; set => _isTaken = value; }
        public CellObjectType ObjectType { get => _objectType; set => _objectType = value; }

        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
            _objectType = CellObjectType.Empty;
            _isTaken = false;
        }
    }


    public enum CellObjectType
    {
        Empty,
        Road,
        Obstacle,
        Building,
        Start,
        Exit
    }
}

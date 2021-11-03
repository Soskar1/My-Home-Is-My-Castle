using UnityEngine;
using Map.Grid;

namespace Map.Generation
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GridVisualizer _gridVisualizer;
        [SerializeField] private MapVisualizer _mapVisualizer;
        private CandidateMap _map;

        [SerializeField] private Direction _startEdge;
        [SerializeField] private Direction _exitEgde;

        [SerializeField] private bool _randomPlacement;
        [SerializeField] private int _numberOfPieces;

        [SerializeField] private bool _autoRepair;

        private Vector2 _startPosition;
        private Vector2 _exitPosition;

        [SerializeField] private int _width;
        [SerializeField] private int _length;

        public MapGrid grid;
        public MapData data;

        private void Start() => GenerateNewMap();

        public void GenerateNewMap()
        {
            _mapVisualizer.ClearMap();
            _gridVisualizer.ClearGrid();

            grid = new MapGrid(_width, _length);

            Map.RandomlyChooseAndSetStartAndExit(grid, ref _startPosition, ref _exitPosition, _randomPlacement, _startEdge, _exitEgde);

            _map = new CandidateMap(grid, _numberOfPieces);
            _map.CreateMap(_startPosition, _exitPosition, _autoRepair);
            _mapVisualizer.VisualizeMap(grid, _map.ReturnMapData());
            _gridVisualizer.VisualizeGrid(grid);

            data = _map.ReturnMapData();
        }

        public void TryRepair()
        {
            if (_map != null)
            {
                var obstaclesToRemove = _map.Repair();
                if (obstaclesToRemove.Count > 0)
                {
                    _mapVisualizer.ClearMap();
                    _gridVisualizer.ClearGrid();

                    _mapVisualizer.VisualizeMap(grid, _map.ReturnMapData());
                    _gridVisualizer.VisualizeGrid(grid);
                }
            }
        }
    }
}

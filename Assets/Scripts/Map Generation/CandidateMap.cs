using UnityEngine;
using System.Collections.Generic;
using Map.AI;
using System.Linq;
using Map.Grid;

namespace Map.Generation
{
    public class CandidateMap
    {
        private MapGrid _grid;
        private int _numberOfPieces;
        private bool[] _obstaclesArray = null;

        private Vector2 _startPoint;
        private Vector2 _exitPoint;

        private List<KnightPiece> _knightPieces;
        private List<Vector2> _path = new List<Vector2>();

        public MapGrid Grid { get => _grid;}
        public bool[] ObstaclesArray { get => _obstaclesArray; }

        public CandidateMap(MapGrid grid, int numberOfPieces)
        {
            _numberOfPieces = numberOfPieces;
            _grid = grid;
        }

        public void CreateMap(Vector2 startPos, Vector2 exitPos, bool autoRepair = false)
        {
            _startPoint = startPos;
            _exitPoint = exitPos;
            _obstaclesArray = new bool[_grid.Width * _grid.Length];

            _knightPieces = new List<KnightPiece>();
            RandomlyPlaceKnightPieces(_numberOfPieces);
            PlaceObstacles();

            FindPath();

            if (autoRepair)
                Repair();
        }

        private void FindPath() => _path = Astar.GetPath(_startPoint, _exitPoint, _obstaclesArray, _grid);

        private bool CheckIfPositionCanBeObstacle(Vector2 position)
        {
            if (position == _startPoint || position == _exitPoint)
                return false;

            int index = _grid.CalculateIndexFromCoordinates(position);

            return _obstaclesArray[index] == false;
        }

        private void RandomlyPlaceKnightPieces(int numberOfPieces)
        {
            int count = numberOfPieces;
            int knightPlacementLimit = 100;

            while (count > 0 && knightPlacementLimit > 0)
            {
                var randomIndex = Random.Range(0, _obstaclesArray.Length);
                if (!_obstaclesArray[randomIndex])
                {
                    Vector2 coordinates = _grid.CalculateCoordinatesFromIndex(randomIndex);

                    if (coordinates == _startPoint || coordinates == _exitPoint)
                        continue;

                    _obstaclesArray[randomIndex] = true;
                    _knightPieces.Add(new KnightPiece(coordinates));

                    count--;
                }

                knightPlacementLimit--;
            }
        }

        private void PlaceObstaclesForKnight(KnightPiece knight)
        {
            foreach (var position in KnightPiece.possibleMoves)
            {
                var newPosition = knight.Position + position;

                if (_grid.IsCellValid(newPosition) && CheckIfPositionCanBeObstacle(newPosition))
                    _obstaclesArray[_grid.CalculateIndexFromCoordinates(newPosition)] = true;
            }
        }

        private void PlaceObstacles()
        {
            foreach (var knight in _knightPieces)
                PlaceObstaclesForKnight(knight);
        }

        public MapData ReturnMapData()
        {
            return new MapData
            {
                obstacleArr = _obstaclesArray,
                knightPieces = _knightPieces,
                startPos = _startPoint,
                exitPos = _exitPoint,
                path = _path
            };
        }

        public List<Vector2> Repair()
        {
            int numberOfObstacles = _obstaclesArray.Where(obstacle => obstacle).Count();
            List<Vector2> obstaclesToRemove = new List<Vector2>();

            if (_path.Count <= 0)
            {
                do
                {
                    int obstacleIndexToRemove = Random.Range(0, numberOfObstacles);
                    for (int i = 0; i < _obstaclesArray.Length; i++)
                    {
                        if (_obstaclesArray[i])
                        {
                            if (obstacleIndexToRemove == 0)
                            {
                                _obstaclesArray[i] = false;
                                obstaclesToRemove.Add(_grid.CalculateCoordinatesFromIndex(i));
                                break;
                            }
                            obstacleIndexToRemove--;
                        }
                    }

                    FindPath();
                } while (_path.Count <= 0);
            }

            foreach (var obstaclePosition in obstaclesToRemove)
            {
                if (!_path.Contains(obstaclePosition))
                {
                    int index = _grid.CalculateIndexFromCoordinates(obstaclePosition);
                    _obstaclesArray[index] = true;
                }
            }

            return obstaclesToRemove;
        }
    }
}

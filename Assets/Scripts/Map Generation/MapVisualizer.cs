using System.Collections.Generic;
using UnityEngine;
using Map.Grid;

namespace Map.Generation
{
    public class MapVisualizer : MonoBehaviour
    {
        private Transform _parent;

        [SerializeField] private GameObject _roadStraight;
        [SerializeField] private GameObject _roadTileCorner;
        [SerializeField] private GameObject _tileEmpty;
        [SerializeField] private GameObject _startTile;
        [SerializeField] private GameObject _exitTile;
        [SerializeField] private GameObject[] _environmentTiles;

        private Dictionary<Vector2, GameObject> _tiles = new Dictionary<Vector2, GameObject>();

        private Vector2 _additionalPosition = new Vector2(.5f, .5f);

        #region BaseBuildings
        [SerializeField] private List<GameObject> _baseBuildings;

        private List<Vector2> _possibleTiles = new List<Vector2>
        {
            Vector2.up,
            new Vector2(1, 1),
            Vector2.right,
            new Vector2(1, -1),
            Vector2.down,
            new Vector2(-1, -1),
            Vector2.left,
            new Vector2(-1, 1),
            new Vector2(0, 2),
            new Vector2(1, 2),
            new Vector2(2, 2),
            new Vector2(2, 1),
            new Vector2(2, 0),
            new Vector2(2, -1),
            new Vector2(2, -2),
            new Vector2(1, -2),
            new Vector2(0, -2),
            new Vector2(-1, -2),
            new Vector2(-2, -2),
            new Vector2(-2, -1),
            new Vector2(-2, 0),
            new Vector2(-2, 1),
            new Vector2(-2, 2),
            new Vector2(-1, 2)
        };
        private Vector2[] _possiblePositions;
        #endregion

        private void Awake() => _parent = this.transform;

        public void VisualizeMap(MapGrid grid, in MapData data)
        {
            for (int index = 0; index  < data.path.Count; index ++)
            {
                var position = data.path[index];

                if (position != data.exitPos)
                    grid.SetCell(position, CellObjectType.Road);
            }

            for (int col = 0; col < grid.Width; col++)
            {
                for (int row = 0; row < grid.Length; row++)
                {
                    var cell = grid.GetCell(col, row);
                    var position = new Vector2(cell.X, cell.Y);

                    var index = grid.CalculateIndexFromCoordinates(position);
                    if (data.obstacleArr[index] && !cell.IsTaken)
                        grid.SetCell(position, CellObjectType.Obstacle);

                    Direction previousDirection = Direction.None;
                    Direction nextDirection = Direction.None;

                    switch (cell.ObjectType)
                    {
                        case CellObjectType.Empty:
                            CreateIndicator(position, _tileEmpty);
                            break;
                        case CellObjectType.Road:
                            if (data.path.Count > 0)
                            {
                                previousDirection = GetDirectionOfPreviousCell(position, data);
                                nextDirection = GetDirectionOfNextCell(position, data);
                            }

                            if (previousDirection == Direction.Up && nextDirection == Direction.Right ||
                                previousDirection == Direction.Right && nextDirection == Direction.Up)
                                CreateIndicator(position, _roadTileCorner, Quaternion.Euler(0, 0, 90));
                            else if (previousDirection == Direction.Right && nextDirection == Direction.Down ||
                                previousDirection == Direction.Down && nextDirection == Direction.Right)
                                CreateIndicator(position, _roadTileCorner);
                            else if (previousDirection == Direction.Down && nextDirection == Direction.Left ||
                                previousDirection == Direction.Left && nextDirection == Direction.Down)
                                CreateIndicator(position, _roadTileCorner, Quaternion.Euler(0, 0, -90));
                            else if (previousDirection == Direction.Left && nextDirection == Direction.Up ||
                                previousDirection == Direction.Up && nextDirection == Direction.Left)
                                CreateIndicator(position, _roadTileCorner, Quaternion.Euler(0, 0, 180));
                            else if (previousDirection == Direction.Right && nextDirection == Direction.Left ||
                                previousDirection == Direction.Left && nextDirection == Direction.Right)
                                CreateIndicator(position, _roadStraight, Quaternion.Euler(0, 0, 90));
                            else
                                CreateIndicator(position, _roadStraight);

                            break;
                        case CellObjectType.Obstacle:
                            int randomIndex = Random.Range(0, _environmentTiles.Length);
                            CreateIndicator(position, _environmentTiles[randomIndex]);
                            break;
                        case CellObjectType.Start:
                            if (data.path.Count > 0)
                                nextDirection = GetDirectionFromVectors(data.path[0], position);

                            if (nextDirection == Direction.Right || nextDirection == Direction.Left)
                                CreateIndicator(position, _startTile, Quaternion.Euler(0, 0, 90));
                            else
                                CreateIndicator(position, _startTile);
                            break;
                        case CellObjectType.Exit:
                            CreateIndicator(position, _exitTile);
                            break;
                        default:
                            break;
                    }
                }
            }

            Vector2 center = new Vector2(grid.Width / 2, grid.Length / 2);
            _possiblePositions = new Vector2[_baseBuildings.Count];
            for (int index = 0; index < _baseBuildings.Count; index++)
            {
                while (_possiblePositions[index] == Vector2.zero)
                {
                    Vector2 randomPossibleTile = _possibleTiles[Random.Range(0, _possibleTiles.Count)];
                    Vector2 possiblePosition = center + randomPossibleTile;

                    Cell possibleCell = grid.GetCell(possiblePosition);
                    if (possibleCell.ObjectType != CellObjectType.Road &&
                        possibleCell.ObjectType != CellObjectType.Building)
                    {
                        _possiblePositions[index] = possiblePosition;
                        ReplaceIndicator(possiblePosition, _baseBuildings[index]);
                        grid.SetCell(possiblePosition, CellObjectType.Building);
                    }
                }
            }
        }

        private void CreateIndicator(Vector2 position, GameObject prefab, Quaternion rotation = new Quaternion())
        {
            var placementPosition = position + _additionalPosition;
            var element = Instantiate(prefab, placementPosition, rotation);
            element.transform.parent = _parent;
            _tiles.Add(position, element);
        }

        public void ReplaceIndicator(Vector2 position, GameObject replaceTo)
        {
            if (_tiles.TryGetValue(position, out GameObject currentTile))
            {
                Destroy(currentTile);
                _tiles.Remove(position);

                CreateIndicator(position, replaceTo);
            }
            else
            {
                throw new MissingReferenceException("Не найден currentTile!");
            }
        }

        public void ClearMap()
        {
            foreach (var obstacle in _tiles.Values)
                Destroy(obstacle);

            _tiles.Clear();
        }

        private Direction GetDirectionFromVectors(Vector2 positionToGoTo, Vector2 position)
        {
            if (positionToGoTo.x > position.x)
                return Direction.Right;
            else if (positionToGoTo.x < position.x)
                return Direction.Left;
            else if (positionToGoTo.y < position.y)
                return Direction.Down;

            return Direction.Up;
        }

        private Direction GetDirectionOfPreviousCell(Vector2 position, MapData data)
        {
            var index = data.path.FindIndex(a => a == position);
            var previousCellPosition = Vector2.zero;

            if (index > 0)
                previousCellPosition = data.path[index - 1];
            else
                previousCellPosition = data.startPos;

            return GetDirectionFromVectors(previousCellPosition, position);
        }

        private Direction GetDirectionOfNextCell(Vector2 position, MapData data)
        {
            int index = data.path.FindIndex(a => a == position);
            var nextCellPosition = data.path[index + 1];

            return GetDirectionFromVectors(nextCellPosition, position);
        }
    }
}

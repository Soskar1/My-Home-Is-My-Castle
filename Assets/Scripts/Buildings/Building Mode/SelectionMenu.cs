using System.Collections.Generic;
using UnityEngine;
using Map.Grid;
using Map.Generation;
using Main;

namespace Buildings.Placement
{
    public class SelectionMenu : MonoBehaviour
    {
        [Header("Game")]
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private Game _game;
        private Controls _controls;

        [Header("Selection Menu Options")]
        [SerializeField] private GameObject _emptyTileOptions;
        [SerializeField] private GameObject _obstacleTileOptions;
        [SerializeField] private GameObject _buildingTileOptions;
        private Dictionary<CellObjectType, GameObject> _optionsForEachTileType = new Dictionary<CellObjectType, GameObject>();

        [Header("Grid")]
        [SerializeField] private GameObject _grid;
        [SerializeField] private GameObject _emptyTile;
        [SerializeField] private GameObject _filledTile;
        private GameObject _currentTile;
        private Vector2 _additionalPosition = new Vector2(.5f, .5f);

        [Header("Projection")]
        [SerializeField] private ProjectionDisplay _projectionDisplay;

        [Header("UI")]
        [SerializeField] private GameObject _finishButton;

        private void Awake()
        {
            _controls = _game.controls;

            _optionsForEachTileType.Add(CellObjectType.Empty, _emptyTileOptions);
            _optionsForEachTileType.Add(CellObjectType.Obstacle, _obstacleTileOptions);
            _optionsForEachTileType.Add(CellObjectType.Building, _buildingTileOptions);
        }

        private void OnEnable()
        {
            _controls.Player.Disable();
            _grid.SetActive(false);
            _finishButton.SetActive(false);
        }

        private void OnDisable()
        {
            _controls.Player.Enable();
            _grid.SetActive(true);
            _finishButton.SetActive(true);

            if (_currentTile != null)
                Destroy(_currentTile);
        }

        public void Activate(Vector2 position, CellObjectType cellObjectType)
        {
            Cell cell = _mapGenerator.grid.GetCell(position);
            Vector2 menuPosition = new Vector2(cell.X, cell.Y) + _additionalPosition;

            gameObject.SetActive(true);
            transform.position = menuPosition;
            //projectionInstance.transform.position = menuPosition;
            _projectionDisplay.projection.transform.position = menuPosition;

            if (cellObjectType != CellObjectType.Empty)
                _currentTile = Instantiate(_filledTile, menuPosition, Quaternion.identity);
            else
                _currentTile = Instantiate(_emptyTile, menuPosition, Quaternion.identity);

            if (_optionsForEachTileType.TryGetValue(cellObjectType, out GameObject option))
                option.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _emptyTileOptions.SetActive(false);
            _buildingTileOptions.SetActive(false);
            _obstacleTileOptions.SetActive(false);
        }
    }
}

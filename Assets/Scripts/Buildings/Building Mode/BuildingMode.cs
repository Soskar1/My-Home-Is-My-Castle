using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Main;
using Map.Grid;
using Buildings.Services;

namespace Buildings.Placement
{
    public class BuildingMode : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private SelectionMenu _selectionMenu;
        [SerializeField] private BuildingPlacement _buildingPlacement;

        private Controls _controls;
        private Action<InputAction.CallbackContext> _handler;

        [SerializeField] private List<OptionButton> _optionButtons;

        private void Awake()
        {
            _handler += CheckClickedPosition;
            _controls = _game.controls;
        }

        private void OnEnable() => _controls.Player.Click.performed += _handler;
        private void OnDisable() => _controls.Player.Click.performed -= _handler;

        private void CheckClickedPosition(InputAction.CallbackContext ctx)
        {
            Vector2 worldPos = _controls.Player.MousePosition.ReadValue<Vector2>();
            worldPos = Camera.main.ScreenToWorldPoint(worldPos);

            CellObjectType clickedCellObjectType = _buildingPlacement.CheckForCellObjectType(worldPos);

            switch (clickedCellObjectType)
            {
                case CellObjectType.Empty:
                    _selectionMenu.Activate(worldPos, clickedCellObjectType);
                    break;
                case CellObjectType.Obstacle:
                    _selectionMenu.Activate(worldPos, clickedCellObjectType);
                    break;
                case CellObjectType.Building:
                    _selectionMenu.Activate(worldPos, clickedCellObjectType);
                    break;
                default:
                    break;
            }
        }
    }
}

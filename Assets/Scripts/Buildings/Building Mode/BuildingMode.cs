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

        private Vector2 _clickPos;

        private void Awake()
        {
            _handler += CheckClickedPosition;
            _controls = _game.controls;
        }

        private void Start()
        {
            foreach (OptionButton optionButton in _optionButtons)
                optionButton.Agreed += ImplementService;
        }

        private void OnEnable() => _controls.Player.Click.performed += _handler;
        private void OnDisable() => _controls.Player.Click.performed -= _handler;

        private void CheckClickedPosition(InputAction.CallbackContext ctx)
        {
            _clickPos = _controls.Player.MousePosition.ReadValue<Vector2>();
            _clickPos = Camera.main.ScreenToWorldPoint(_clickPos);

            CellObjectType clickedCellObjectType = _buildingPlacement.CheckForCellObjectType(_clickPos);

            switch (clickedCellObjectType)
            {
                case CellObjectType.Empty:
                    _selectionMenu.Activate(_clickPos, clickedCellObjectType);
                    break;
                case CellObjectType.Obstacle:
                    _selectionMenu.Activate(_clickPos, clickedCellObjectType);
                    break;
                case CellObjectType.Building:
                    _selectionMenu.Activate(_clickPos, clickedCellObjectType);
                    break;
                default:
                    break;
            }
        }

        private void ImplementService(ServiceData serviceData)
        {
            switch (serviceData.serviceType)
            {
                case ServiceType.PlaceBuilding:
                    _buildingPlacement.Build(serviceData.entity, _clickPos);
                    break;
                case ServiceType.RemoveObstacle:
                    break;
                default:
                    break;
            }
        }
    }
}

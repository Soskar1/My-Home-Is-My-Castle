using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Buildings.UI
{
    public class Tooltip : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Transform _selectionMenu;
        [SerializeField] private float _additionalPlacementPosition;
        [SerializeField] private float _additionalBorderCheckPosition;

        [Header("Content")]
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;

        [SerializeField] private Image _visual;

        [SerializeField] private GameObject _logs;
        [SerializeField] private TextMeshProUGUI _logCost;

        [SerializeField] private GameObject _rocks;
        [SerializeField] private TextMeshProUGUI _rockCost;

        public void Show(Building building, string description)
        {
            gameObject.SetActive(true);
            PlaceNearSelectionMenu();

            _title.SetText(building.name);
            _description.SetText(description);

            _visual.sprite = building.visual;

            CheckResourceRequirements(_logs, building.logCost);
            _logCost.SetText(building.logCost.ToString());

            CheckResourceRequirements(_rocks, building.rockCost);
            _rockCost.SetText(building.rockCost.ToString());
        }

        private void CheckResourceRequirements(GameObject resource, int quantity)
        {
            if (quantity > 0)
                resource.SetActive(true);
            else
                resource.SetActive(false);
        }

        private void PlaceNearSelectionMenu()
        {
            Vector2 maxBounds = CameraExtensions.BoundsMax();
            Vector2 placement;

            //rightSide
            placement = new Vector2(_selectionMenu.position.x + _additionalPlacementPosition,
                _selectionMenu.position.y);

            if (placement.x + _additionalBorderCheckPosition < maxBounds.x)
            {
                transform.position = placement;
                return;
            }

            //leftSide
            placement = new Vector2(_selectionMenu.position.x - _additionalPlacementPosition,
                _selectionMenu.position.y);

            transform.position = placement;
        }
    }
}

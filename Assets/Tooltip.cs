using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Buildings;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;

    [SerializeField] private Image _visual;

    [SerializeField] private GameObject _logs;
    [SerializeField] private TextMeshProUGUI _logCost;

    [SerializeField] private GameObject _rocks;
    [SerializeField] private TextMeshProUGUI _rockCost;

    public void Show(Building building, string description)
    {
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
}

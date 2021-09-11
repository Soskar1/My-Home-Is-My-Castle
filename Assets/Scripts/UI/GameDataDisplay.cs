using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Buildings.Resources;
using Main.Information;

public class GameDataDisplay : MonoBehaviour
{
    [SerializeField] private GameData _gameData;

    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _logsText;
    [SerializeField] private TextMeshProUGUI _rocksText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    private Dictionary<Resource, TextMeshProUGUI> _resourceText = new Dictionary<Resource, TextMeshProUGUI>();

    private Resource _logs;
    private Resource _rocks;
    private Resource _coins;

    private void Start()
    {
        _logs = _gameData.ReturnResource(ResourceType.Log);
        _rocks = _gameData.ReturnResource(ResourceType.Rock);
        _coins = _gameData.ReturnResource(ResourceType.Coin);

        _resourceText.Add(_logs, _logsText);
        _resourceText.Add(_rocks, _rocksText);
        _resourceText.Add(_coins, _coinsText);

        _logs.ResourceReceived += Display;
        _rocks.ResourceReceived += Display;

        _waveText.SetText("Wave 0");
        _healthText.SetText("0");
        _logsText.SetText(_logs.Quantity.ToString());
        _rocksText.SetText(_rocks.Quantity.ToString());
        _coinsText.SetText(_coins.Quantity.ToString());
    }

    private void Display(Resource resource)
    {
        if (_resourceText.TryGetValue(resource, out TextMeshProUGUI text))
            text.SetText(resource.Quantity.ToString());
        else
            throw new System.Exception("не нахожу текст");
    }
}
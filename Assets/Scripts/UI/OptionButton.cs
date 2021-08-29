using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Buildings.Services;
using Buildings.Resources;
using Main.Information;

namespace Buildings.Placement
{
    public class OptionButton : MonoBehaviour
    {
        [SerializeField] private GameData _data;
        [SerializeField] private Service _service;
        public Action Agreed;

        private UnityAction _action;

        [SerializeField] private Image _visual;
        [SerializeField] private Sprite _agree;
        [SerializeField] private Sprite _defaultVisual;
        [SerializeField] private Button _button;

        private bool _canAgree;

        private void Awake()
        {
            _action = new UnityAction(NeedToAgree);
            _button.onClick.AddListener(_action);
        }

        private void OnEnable() => _canAgree = CheckBuyingOpportunity();
        private void OnDisable() => ButtonReset();

        private void SetButtonVisual(Sprite visual) => _visual.sprite = visual;

        private void NeedToAgree()
        {
            _button.onClick.RemoveListener(_action);
            _action -= NeedToAgree;
            _action += Agree;
            _button.onClick.AddListener(_action);

            SetButtonVisual(_agree);

            _button.interactable = _canAgree;
        }

        private void Agree()
        {
            Agreed?.Invoke();
            ButtonReset();
        }

        public void ButtonReset()
        {
            _button.onClick.RemoveListener(_action);
            _action += NeedToAgree;
            _action -= Agree;
            _button.onClick.AddListener(_action);

            SetButtonVisual(_defaultVisual);

            _button.interactable = true;
        }

        private bool CheckBuyingOpportunity()
        {
            Resource resource = _data.ReturnResource(ResourceType.Log);

            if (CostCheck(_service.logCost, resource.Quantity))
            {
                resource = _data.ReturnResource(ResourceType.Rock);
                if (CostCheck(_service.rockCost, resource.Quantity))
                    return true;
            }

            return false;
        }

        private bool CostCheck(int cost, int resourceCount)
        {
            if (resourceCount >= cost)
                return true;

            return false;
        }
    }
}

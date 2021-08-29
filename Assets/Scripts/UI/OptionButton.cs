using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Buildings.Services;

namespace Buildings.Placement
{
    public class OptionButton : MonoBehaviour
    {
        [SerializeField] private SelectionMenu _selectionMenu;
        [SerializeField] private Service _service;
        public Action<ServiceData> Agreed;

        private UnityAction _action;

        [SerializeField] private Image _visual;
        [SerializeField] private Sprite _agree;
        [SerializeField] private Sprite _defaultVisual;
        [SerializeField] private Button _button;

        private bool _canAgree;
        private bool _needToAgree = true;

        private void Awake()
        {
            _action = new UnityAction(NeedToAgree);
            _button.onClick.AddListener(_action);
        }

        private void OnEnable() => _canAgree = _service.CheckBuyingOpportunity();
        private void OnDisable() => ButtonReset();

        private void SetButtonVisual(Sprite visual) => _visual.sprite = visual;

        private void NeedToAgree()
        {
            if (_needToAgree)
            {
                _button.onClick.RemoveListener(_action);
                _action -= NeedToAgree;
                _action += Agree;
                _button.onClick.AddListener(_action);

                SetButtonVisual(_agree);

                _button.interactable = _canAgree;
                _needToAgree = false;
            }
        }

        private void Agree()
        {
            Agreed?.Invoke(_service.GetServiceData());
            _selectionMenu.Close();
        }

        public void ButtonReset()
        {
            if (!_needToAgree)
            {
                _button.onClick.RemoveListener(_action);
                _action += NeedToAgree;
                _action -= Agree;
                _button.onClick.AddListener(_action);

                SetButtonVisual(_defaultVisual);

                _button.interactable = true;
                _needToAgree = true;
            }
        }
    }
}

using Buildings.Placement;
using UnityEngine;

namespace Buildings.UI
{
    public class TooltipDisplay : MonoBehaviour
    {
        [SerializeField] private Tooltip _tooltip;
        [SerializeField] private Option _option;
        [SerializeField] private Building _building;

        [TextArea]
        [SerializeField] private string _description;

        //Используется в кнопках
        public void Display() => _tooltip.Show(_building, _description);
    }
}

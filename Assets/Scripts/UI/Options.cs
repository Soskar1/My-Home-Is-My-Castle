using UnityEngine;

namespace Buildings.Placement
{
    public class Options : MonoBehaviour
    {
        [SerializeField] private SelectionMenu _selectionMenu;

        private Projection _projection;

        private void Start() => _projection = _selectionMenu.projectionInstance;

        public void SetProjection(Sprite visual) => _projection.SetVisual(visual);
    }
}

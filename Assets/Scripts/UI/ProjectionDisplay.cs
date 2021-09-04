using UnityEngine;

namespace Buildings.Placement
{
    public class ProjectionDisplay : MonoBehaviour
    {
        public Projection projection;

        private void OnDisable() => projection.SetVisual(null);

        //Используется в кнопках
        public void SetProjection(Sprite visual) => projection.SetVisual(visual);
    }
}

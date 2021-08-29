using UnityEngine;

namespace Buildings.Placement
{
    public class Projection : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _visual;

        public void SetVisual(Sprite visual) => _visual.sprite = visual;
    }
}

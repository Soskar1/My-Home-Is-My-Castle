using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(fileName = "new Building", menuName = "Building")]
    public class Building : ScriptableObject
    {
        public int logCost;
        public int rockCost;

        public Sprite visual;
        public GameObject building;
    }
}

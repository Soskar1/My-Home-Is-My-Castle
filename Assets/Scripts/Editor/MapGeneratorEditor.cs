using UnityEngine;
using UnityEditor;

namespace Map.Generation
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        private MapGenerator _map;

        private void OnEnable() => _map = (MapGenerator)target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
            {
                if (GUILayout.Button("Generate new map"))
                    _map.GenerateNewMap();

                if (GUILayout.Button("Repair map"))
                    _map.TryRepair();
            }
        }
    }
}

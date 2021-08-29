using UnityEngine;
using UnityEditor;

namespace Buildings.Services
{
    [CustomEditor(typeof(Service))]
    public class ServiceEditor : Editor
    {
        private Service _service;

        #region int
        private SerializedProperty _logCostProp;
        private SerializedProperty _rockCostProp;
        #endregion

        #region GameObject
        private SerializedProperty _buildingProp;
        #endregion

        #region GameData
        private SerializedProperty _gameDataProp;
        #endregion

        private void OnEnable()
        {
            _gameDataProp = serializedObject.FindProperty("_data");
            _logCostProp = serializedObject.FindProperty("_logCost");
            _rockCostProp = serializedObject.FindProperty("_rockCost");
            _buildingProp = serializedObject.FindProperty("_building");

            _service = (Service)target;   
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            #region Fields
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(_gameDataProp);
            #region ServiceType
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Service Type", GUILayout.Width(230f));
            _service.serviceType = (ServiceType)EditorGUILayout.EnumPopup(_service.serviceType);
            EditorGUILayout.EndHorizontal();
            #endregion
            EditorGUILayout.PropertyField(_logCostProp);
            EditorGUILayout.PropertyField(_rockCostProp);

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            if (_service.serviceType == ServiceType.PlaceBuilding)
                EditorGUILayout.ObjectField(_buildingProp);
            else
                _buildingProp.DeleteCommand();

            EditorGUILayout.EndVertical();
            #endregion

            serializedObject.ApplyModifiedProperties();
        }
    }
}

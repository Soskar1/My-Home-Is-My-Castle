using UnityEngine;
using Main.Information;

namespace Buildings.Resources
{
    public class Extraction : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private int _quantityToExtract;

        private GameData _information;
        private Resource _resource;

        private void Awake()
        {
            _information = GameData.singleton;
            _resource = _information.ReturnResource(_resourceType);
        }

        [ContextMenu("Добыть 100 ресурса")]
        public void Extract()
        {
            _resource.Quantity += 100;
        }
    }
}

using Buildings.Resources;
using Main.Information;
using UnityEngine;

namespace Buildings.Services
{
    public class Service : MonoBehaviour
    {
        [SerializeField] private GameData _data;

        public ServiceData serviceData;

        private Resource _logs;
        private Resource _rocks;

        public void WriteOffTheCost()
        {
            _logs.Quantity -= serviceData.logCost;
            _rocks.Quantity -= serviceData.rockCost;
        }

        public bool CheckBuyingOpportunity()
        {
            if (_logs == null || _rocks == null)
                SetResources();

            if (!CostCheck(serviceData.logCost, _logs.Quantity))
                return false;

            if (!CostCheck(serviceData.rockCost, _rocks.Quantity))
                return false;

            return true;
        }

        private bool CostCheck(int cost, int resourceCount)
        {
            if (resourceCount >= cost)
                return true;

            return false;
        }

        private void SetResources()
        {
            _logs = _data.ReturnResource(ResourceType.Log);
            _rocks = _data.ReturnResource(ResourceType.Rock);
        }
    }

    public enum ServiceType
    { 
        PlaceBuilding,
        RemoveObstacle
    }
}

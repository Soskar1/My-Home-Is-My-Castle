using Buildings.Resources;
using Main.Information;
using UnityEngine;

namespace Buildings.Services
{
    public class Service : MonoBehaviour
    {
        [SerializeField] private GameData _data;

        public ServiceType serviceType;
        [SerializeField] private int _logCost;
        [SerializeField] private int _rockCost;

        [SerializeField] private GameObject _building;

        public ServiceData GetServiceData()
        {
            switch (serviceType)
            {
                case ServiceType.PlaceBuilding:
                    return new ServiceData
                    {
                        logCost = _logCost,
                        rockCost = _rockCost,
                        serviceType = ServiceType.PlaceBuilding,
                        entity = _building
                    };
                case ServiceType.RemoveObstacle:
                    return new ServiceData
                    {
                        logCost = _logCost,
                        rockCost = _rockCost,
                        serviceType = ServiceType.RemoveObstacle
                    };
                default:
                    break;
            }

            return new ServiceData();
        }

        public bool CheckBuyingOpportunity()
        {
            Resource resource = _data.ReturnResource(ResourceType.Log);

            if (CostCheck(_logCost, resource.Quantity))
            {
                resource = _data.ReturnResource(ResourceType.Rock);
                if (CostCheck(_rockCost, resource.Quantity))
                    return true;
            }

            return false;
        }

        private bool CostCheck(int cost, int resourceCount)
        {
            if (resourceCount >= cost)
                return true;

            return false;
        }
    }

    public enum ServiceType
    { 
        PlaceBuilding,
        RemoveObstacle
    }
}

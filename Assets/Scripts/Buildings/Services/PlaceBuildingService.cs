using UnityEngine;

namespace Buildings.Services
{
    public class PlaceBuildingService : Service
    {
        [SerializeField] private Building _building;

        private void Awake()
        {
            serviceData = new ServiceData
            {
                entity = _building.building,
                serviceType = ServiceType.PlaceBuilding,
                logCost = _building.logCost,
                rockCost = _building.rockCost
            };
        }
    }
}

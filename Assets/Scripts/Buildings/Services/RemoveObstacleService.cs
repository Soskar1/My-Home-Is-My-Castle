using UnityEngine;

namespace Buildings.Services
{
    public class RemoveObstacleService : Service
    {
        [SerializeField] private int _logCost;
        [SerializeField] private int _rockCost;

        private void Awake()
        {
            serviceData = new ServiceData
            {
                serviceType = ServiceType.RemoveObstacle,
                logCost = _logCost,
                rockCost = _rockCost
            };
        }
    }
}

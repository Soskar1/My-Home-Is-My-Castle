using Buildings.Resources;
using UnityEngine;

namespace Buildings.Services
{
    public class RemoveObstacleService : Service
    {
        [SerializeField] private GameObject _grassTile;
        [SerializeField] private GameObject _option;
        [SerializeField] private int _coinCost;

        private void Awake()
        {
            serviceData = new ServiceData
            {
                entity = _grassTile,
                serviceType = ServiceType.RemoveObstacle,
                coinCost = _coinCost
            };

            _option.SetActive(true);
        }
    }
}

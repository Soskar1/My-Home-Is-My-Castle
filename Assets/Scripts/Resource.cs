using System;

namespace Buildings.Resources
{
    public class Resource
    {
        private int _quantity;
        private ResourceType _resourceType;

        public Action<Resource> ResourceReceived;

        public int Quantity
        {
            get { return _quantity; }
            set 
            {
                _quantity = value;
                ResourceReceived?.Invoke(this);
            }
        }
        public ResourceType ResourceType { get => _resourceType; }

        public Resource(ResourceType resourceType)
        {
            _quantity = 0;
            _resourceType = resourceType;
        }

        public void Add(int amount)
        {
            if (amount > 0)
                Quantity += amount;
            else
                throw new Exception("Ќевозможно добавить отрицательное число!");
        }
        public void Reduce(int amount)
        {
            if (amount > 0)
                Quantity -= amount;
            else
                throw new Exception("Ќевозможно вычесть отрицательное число!");
        }
    }

    public enum ResourceType
    {
        Log,
        Rock,
        Coin
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using Buildings.Resources;

namespace Main.Information
{
    public class GameData : MonoBehaviour
    {
        public static GameData singleton { get; private set; }

        public Dictionary<ResourceType, Resource> resources = new Dictionary<ResourceType, Resource>();

        private void Awake()
        {
            singleton = this;

            int resourceTypeNumber = Enum.GetNames(typeof(ResourceType)).Length;

            for (int index = 0; index < resourceTypeNumber; index++)
            {
                ResourceType resourceType = (ResourceType)index;
                Resource resource = new Resource(resourceType);
                resources.Add(resourceType, resource);
            }
        }

        public Resource ReturnResource(ResourceType resourceType)
        {
            if (resources.TryGetValue(resourceType, out Resource resource))
                return resource;
            else
                throw new Exception("Не могу найти ресурс!");
        }
    }
}

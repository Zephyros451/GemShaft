using System;
using System.Collections.Generic;
using System.Linq;
using GemShaft.Data;
using UniRx;
using UnityEngine;

namespace GemShaft.Managers.Screen
{
    public class ResourcesManager : MonoBehaviour
    {
        private readonly List<Resource> resources = new();

        public IReadOnlyList<Resource> Resources => resources;

        public readonly Subject<ResourceType> OnUpdated = new();

        public Resource Get(ResourceType type)
        {
            return resources.FirstOrDefault(x => x.Type == type) ?? new Resource {Type = type, Amount = 0};
        }

        public void AddResource(ResourceType type, int amount)
        {
            if (resources.Any(x => x.Type == type))
            {
                var resource = resources.First(x => x.Type == type);
                resource.Amount += amount;
            }
            else
            {
                resources.Add(new Resource {Type = type, Amount = amount});
            }

            OnUpdated.OnNext(type);
        }

        public bool TryTakeResource(ResourceType type, int amount)
        {
            var resource = resources.FirstOrDefault(x => x.Type == type);

            if (resource == null || resource.Amount < amount)
            {
                return false;
            }

            resource.Amount -= amount;
            OnUpdated.OnNext(type);
            return true;
        }

        public void GainResources(CellType cellType)
        {
            var type = cellType switch
            {
                CellType.Dirt => ResourceType.Dirt,
                CellType.Stone => ResourceType.Cobblestone,
                CellType.Copper => ResourceType.Copper,
                CellType.Gravel => ResourceType.Flint,
                CellType.Diorite => ResourceType.Cobblestone,
                CellType.Andesite => ResourceType.Cobblestone,
                CellType.Iron => ResourceType.Iron,
                CellType.Lapis => ResourceType.Lapis,
                CellType.Amethyst => ResourceType.Amethyst,
                CellType.Gold => ResourceType.Gold,
                CellType.Diamond => ResourceType.Diamond,
                CellType.Deepslate => ResourceType.Deepslate,
                CellType.Netherite => ResourceType.Netherite,
                _ => throw new ArgumentOutOfRangeException(nameof(cellType), cellType, null)
            };

            AddResource(type, 1);
        }

        public ResourceType GetMainResource(Tier tier)
        {
            return tier switch
            {
                Tier.Tier1 => ResourceType.Dirt,
                Tier.Tier2 => ResourceType.Cobblestone,
                Tier.Tier3 => ResourceType.Iron,
                Tier.Tier4 => ResourceType.Gold,
                Tier.Tier5 => ResourceType.Diamond,
                Tier.Tier6 => ResourceType.Netherite,
                _ => throw new ArgumentOutOfRangeException(nameof(tier), tier, null)
            };
        }

        public ResourceType GetUpgradeResource(Tier tier)
        {
            return tier switch
            {
                Tier.Tier1 => ResourceType.Cobblestone,
                Tier.Tier2 => ResourceType.Iron,
                Tier.Tier3 => ResourceType.Gold,
                Tier.Tier4 => ResourceType.Diamond,
                Tier.Tier5 => ResourceType.Netherite,
                Tier.Tier6 => ResourceType.Netherite,
                _ => throw new ArgumentOutOfRangeException(nameof(tier), tier, null)
            };
        }
    }

    public class Resource
    {
        public ResourceType Type;
        public int Amount;
    }
}
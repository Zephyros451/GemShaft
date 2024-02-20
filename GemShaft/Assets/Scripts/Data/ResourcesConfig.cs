using System.Linq;
using UnityEngine;

namespace GemShaft.Data
{
    [CreateAssetMenu(menuName = "Data/ResourcesConfig", fileName = "ResourcesConfig", order = 0)]
    public class ResourcesConfig : ScriptableObject
    {
        [SerializeField] private ResourceData[] resources;

        public ResourceData GetData(ResourceType type)
        {
            return resources.First(x => x.Type == type);
        }
    }

    [System.Serializable]
    public class ResourceData
    {
        [field: SerializeField] public ResourceType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }

    public enum ResourceType
    {
        Dirt = 0,
        Cobblestone = 1,
        Copper = 2,
        Flint = 3,
        Iron = 4,
        Lapis = 5,
        Amethyst = 6,
        Gold = 7,
        Diamond = 8,
        Deepslate = 9,
        Netherite = 10
    }
}
using System.Linq;
using UnityEngine;

namespace GemShaft.Data
{
    [CreateAssetMenu(menuName = "Data/PickaxesConfig", fileName = "PickaxesConfig", order = 0)]
    public class PickaxesConfig : ScriptableObject
    {
        [SerializeField] private PickaxeData[] pickaxes;

        public PickaxeData GetData(Tier tier)
        {
            return pickaxes.First(x => x.Tier == tier);
        }
    }

    [System.Serializable]
    public class PickaxeData
    {
        [field: SerializeField] public Tier Tier { get; private set; }
        [field: SerializeField] public float Efficiency {get; private set; }
        [field: SerializeField] public float Durability { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Sprite BrokenSprite { get; private set; }
    }

    public enum Tier
    {
        Tier1 = 1,
        Tier2 = 2,
        Tier3 = 3,
        Tier4 = 4,
        Tier5 = 5,
        Tier6 = 6
    }
}
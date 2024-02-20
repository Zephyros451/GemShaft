using System.Linq;
using UnityEngine;

namespace GemShaft.Data
{
    [CreateAssetMenu(menuName = "Art/SoundConfig", fileName = "SoundConfig", order = 0)]
    public class SoundConfig : ScriptableObject
    {
        [SerializeField] private SoundData[] sounds;

        public SoundData GetData(SoundType type)
        {
            return sounds.First(x => x.Type == type);
        }
    }

    [System.Serializable]
    public class SoundData
    {
        [field: SerializeField] public SoundType Type { get; private set; }
        [field: SerializeField] public float Volume { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }
    }

    public enum SoundType
    {
        ButtonClick = 0,
        BlockDigging = 1,
        BlockDestroyed = 2,
        Upgrade = 3,
        Repair = 4,
        BrokenPickaxe = 5,
        OpenInventory = 6,
        CloseInventory = 7
    }
}
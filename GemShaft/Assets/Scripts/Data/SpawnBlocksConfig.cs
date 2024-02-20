using System.Linq;
using UnityEngine;

namespace GemShaft.Data
{
    [CreateAssetMenu(menuName = "Data/SpawnBlocksConfig", fileName = "SpawnBlocksConfig", order = 0)]
    public class SpawnBlocksConfig : ScriptableObject
    {
        [SerializeField] private SpawnBlocksData[] data;

        public SpawnBlocksData GetData(long score)
        {
            return data.LastOrDefault(x => x.StartScore <= score) ?? data.Last();
        }
    }

    [System.Serializable]
    public class SpawnBlocksData
    {
        [field: SerializeField] public int StartScore { get; private set; }
        [field: SerializeField] public string Weights { get; private set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GemShaft.Data
{
    [CreateAssetMenu(menuName = "Data/CellsConfig", fileName = "CellsConfig", order = 0)]
    public class CellsConfig : ScriptableObject
    {
        [SerializeField] private CellData[] cells;

        public CellData GetData(CellType type)
        {
            return cells.First(x => x.Type == type);
        }

        public List<CellData> GetAvailableCells(long score)
        {
            return cells.ToList();
        }
    }

    [System.Serializable]
    public class CellData
    {
        [field: SerializeField] public CellType Type { get; private set; }
        [field: SerializeField] public float Durability { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }

    public enum CellType
    {
        Dirt = 0,
        Stone = 1,
        Copper = 2,
        Gravel = 3,
        Diorite = 4,
        Andesite = 5,
        Iron = 6,
        Lapis = 7,
        Amethyst = 8,
        Gold = 9,
        Diamond = 10,
        Deepslate = 11,
        Netherite = 12
    }
}

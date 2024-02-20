using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GemShaft.Data;
using GemShaft.Managers;
using GemShaft.Managers.Screen;
using TMPro;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GemShaft.Gameplay
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private ResourcesManager resourcesManager;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private HotBar hotBar;
        [SerializeField] private TextMeshProUGUI scoreLabel;
        
        [SerializeField] private CellsConfig cellsConfig;
        [SerializeField] private SpawnBlocksConfig spawnBlocksConfig;
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private Transform content;
        
        [Space]
        [SerializeField] private int width;
        [SerializeField] private int height;

        private Cell[,] cells;
        private long score;

        private void Start()
        {
            SetUp();
            UpdateScore();
        }

        private void SetUp()
        {
            cells = new Cell[width, height];

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var cell = Instantiate(cellPrefab, content);
                    cell.Inject(resourcesManager, soundManager, hotBar, this);
                    cell.Init(GenerateCellData());
                    cell.SetIndex(i, j);
                    cell.SetPosition(width, i, j);
                    cell.Destroyed.Subscribe(OnCellDestroyed).AddTo(cell);
                    cell.PremiumClicked.Subscribe(OnCellPremiumClicked).AddTo(cell);
                    cell.name = $"[{i},{j}]";
                    cells[i, j] = cell;
                }
            }
        }

        private void OnCellDestroyed(Vector2Int position)
        {
            score++;
            UpdateScore();
            cells[position.x, position.y].SetPosition(width, position.x, height);

            RearrangeCells(position.x, position.y);
            CellsAnimation(position.x, position.y);
        }

        private void OnCellPremiumClicked(Vector2Int position)
        {
            for (var i = 0; i < width; i++)
            {
                cells[i, position.y].ManualClicked();
            }
        }

        private void CellsAnimation(int column, int row)
        {
            var sequence = DOTween.Sequence();

            sequence.AppendInterval(0.1f);
            sequence.AppendInterval(0f);

            for (var i = row; i < height; i++)
            {
                sequence.Join(cells[column, i].RectTransform
                    .DOAnchorPosY(cells[column, i].CalculateCellRowPosition(i), 0.2f)
                    .SetEase(Ease.OutBounce));
            }
        }

        private void RearrangeCells(int column, int row)
        {
            var temp = cells[column, row];

            for (var i = row + 1; i < height; i++)
            {
                cells[column, i - 1] = cells[column, i];
                cells[column, i - 1].SetIndex(column, i - 1);
            }

            cells[column, height - 1] = temp;
            cells[column, height - 1].SetIndex(column, height - 1);
            cells[column, height - 1].Init(GenerateCellData());
        }

        private CellData GenerateCellData()
        {
            var spawnBlocksData = spawnBlocksConfig.GetData(score);
            var weights = spawnBlocksData.Weights.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)).ToList();

            var items = new List<int>(weights.Count);
            for (var i = 0; i < weights.Count; i++)
            {
                var weight = weights[i];
                for (var j = 0; j < weight; j++)
                {
                    items.Add(i);
                }
            }

            var randomIndex = Random.Range(0, items.Count);

            return cellsConfig.GetData((CellType)items[randomIndex]);
        }

        private void UpdateScore()
        {
            scoreLabel.text = score.ToString();
        }
    }
}
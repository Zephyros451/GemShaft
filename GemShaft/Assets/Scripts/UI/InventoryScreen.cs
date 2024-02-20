using System.Collections.Generic;
using GemShaft.Data;
using GemShaft.Managers;
using GemShaft.Managers.Screen;
using UniRx;
using UnityEngine;

namespace GemShaft.UI
{
    public class InventoryScreen : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private ResourcesConfig resourcesConfig;
        [SerializeField] private ResourcesManager resourcesManager;
        [SerializeField] private ScreenManager screenManager;
        
        [Space]
        [SerializeField] private Button backButton;
        [SerializeField] private Button repairButton;
        [SerializeField] private Button craftButton;

        [Space]
        [SerializeField] private InventoryItem itemPrefab;
        [SerializeField] private Transform content;

        private readonly List<InventoryItem> items = new();

        private void Start()
        {
            backButton.Clicked.Subscribe(OnBackClicked).AddTo(this);
            repairButton.Clicked.Subscribe(OnRepairClicked).AddTo(this);
            craftButton.Clicked.Subscribe(OnCraftClicked).AddTo(this);
        }

        private void OnEnable()
        {
            foreach (var item in items)
            {
                Destroy(item.gameObject);
            }
            items.Clear();

            foreach (var resource in resourcesManager.Resources)
            {
                var item = Instantiate(itemPrefab, content);
                var data = resourcesConfig.GetData(resource.Type);
                item.Init(resource, data);
                items.Add(item);
            }
        }

        private void OnBackClicked(Unit _)
        {
            soundManager.PlaySound(SoundType.CloseInventory);
            screenManager.OpenScreen(ScreenType.Mine);
        }

        private void OnRepairClicked(Unit _)
        {
            screenManager.OpenScreen(ScreenType.Repair);
        }

        private void OnCraftClicked(Unit _)
        {
            screenManager.OpenScreen(ScreenType.Craft);
        }

        private void Reset()
        {
            screenManager = FindObjectOfType<ScreenManager>();
        }
    }
}
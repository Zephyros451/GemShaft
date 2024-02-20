using GemShaft.Data;
using GemShaft.Managers;
using GemShaft.Managers.Screen;
using TMPro;
using UniRx;
using UnityEngine;

namespace GemShaft.UI
{
    public class MineScreen : MonoBehaviour
    {
        [SerializeField] private ResourcesManager resourcesManager;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private ScreenManager screenManager;

        [Space]
        [SerializeField] private Button inventoryButton;
        [SerializeField] private Button storeButton;

        [SerializeField] private TextMeshProUGUI balance;

        private void Start()
        {
            inventoryButton.Clicked.Subscribe(OnInventoryClicked).AddTo(this);
            storeButton.Clicked.Subscribe(OnStoreClicked).AddTo(this);

            resourcesManager.OnUpdated.Subscribe(UpdateBalance).AddTo(this);
            UpdateBalance(ResourceType.Amethyst);
        }

        private void UpdateBalance(ResourceType type)
        {
            if (type == ResourceType.Amethyst)
            {
                balance.text = resourcesManager.Get(type).Amount.ToString();
            }
        }

        private void OnInventoryClicked(Unit _)
        {
            soundManager.PlaySound(SoundType.OpenInventory);
            screenManager.OpenScreen(ScreenType.Inventory);
        }

        private void OnStoreClicked(Unit _)
        {
            screenManager.OpenScreen(ScreenType.Store);
        }

        private void Reset()
        {
            screenManager = FindObjectOfType<ScreenManager>();
        }
    }
}
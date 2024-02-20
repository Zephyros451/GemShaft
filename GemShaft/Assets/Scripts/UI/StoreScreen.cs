using GemShaft.Data;
using GemShaft.Gameplay;
using GemShaft.Managers.Screen;
using TMPro;
using UniRx;
using UnityEngine;

namespace GemShaft.UI
{
    public class StoreScreen : MonoBehaviour
    {
        [SerializeField] private ResourcesConfig resourcesConfig;
        [SerializeField] private ResourcesManager resourcesManager;
        [SerializeField] private ScreenManager screenManager;
        [SerializeField] private HotBar hotBar;
        
        [Space]
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI balance;
        [SerializeField] private StoreItem[] storeItems;
        [SerializeField] private Button packButton;

        private void Start()
        {
            backButton.Clicked.Subscribe(OnBackClicked).AddTo(this);
            packButton.Clicked.Subscribe(OnPackClicked).AddTo(this);
            resourcesManager.OnUpdated.Subscribe(UpdateBalance).AddTo(this);
            UpdateBalance(ResourceType.Amethyst);

            foreach (var storeItem in storeItems)
            {
                storeItem.Init(resourcesConfig, resourcesManager);
            }
        }

        private void OnBackClicked(Unit _)
        {
            screenManager.OpenScreen(ScreenType.Mine);
        }

        private void OnPackClicked(Unit _)
        {
            resourcesManager.AddResource(ResourceType.Amethyst, 100);
            hotBar.SetPremiumPickaxe();
        }

        private void UpdateBalance(ResourceType type)
        {
            if (type == ResourceType.Amethyst)
            {
                balance.text = resourcesManager.Get(ResourceType.Amethyst).Amount.ToString();
            }
        }
    }
}
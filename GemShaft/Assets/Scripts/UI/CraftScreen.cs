using GemShaft.Data;
using GemShaft.Gameplay;
using GemShaft.Managers;
using GemShaft.Managers.Screen;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GemShaft.UI
{
    public class CraftScreen : MonoBehaviour
    {
        [SerializeField] private PickaxesConfig pickaxesConfig;
        [SerializeField] private ResourcesConfig resourcesConfig;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private HotBar hotBar;
        [SerializeField] private ResourcesManager resourcesManager;
        [SerializeField] private ScreenManager screenManager;
        
        [Space]
        [SerializeField] private Button backButton;
        [SerializeField] private Button craftButton;

        [Space]
        [SerializeField] private Image brokenIcon;
        [SerializeField] private Image upgradedIcon;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private Image priceIcon;

        private void Start()
        {
            backButton.Clicked.Subscribe(OnBackClicked).AddTo(this);
            craftButton.Clicked.Subscribe(OnUpgradeClicked).AddTo(this);
        }

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            if (hotBar.Pickaxe == null)
            {
                return;
            }
            
            var data = pickaxesConfig.GetData(hotBar.Pickaxe.Tier);
            var nextTier = hotBar.Pickaxe.Tier == Tier.Tier6 ? Tier.Tier6 : hotBar.Pickaxe.Tier + 1;
            var upgradeData = pickaxesConfig.GetData(nextTier);
            var resourceType = resourcesManager.GetUpgradeResource(hotBar.Pickaxe.Tier);

            brokenIcon.sprite = data.Sprite;
            upgradedIcon.sprite = upgradeData.Sprite;
            price.text = $"{resourcesManager.Get(resourceType).Amount}/10";
            priceIcon.sprite = resourcesConfig.GetData(resourceType).Icon;
        }

        private void OnBackClicked(Unit _)
        {
            screenManager.OpenScreen(ScreenType.Inventory);
        }

        private void OnUpgradeClicked(Unit _)
        {
            if(resourcesManager.TryTakeResource(resourcesManager.GetUpgradeResource(hotBar.Pickaxe.Tier), 10))
            {
                soundManager.PlaySound(SoundType.Upgrade);
                hotBar.Upgrade();
                UpdateView();
            }
        }

        private void Reset()
        {
            screenManager = FindObjectOfType<ScreenManager>();
        }
    }
}
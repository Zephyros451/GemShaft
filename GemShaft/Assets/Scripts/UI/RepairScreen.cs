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
    public class RepairScreen : MonoBehaviour
    {
        [SerializeField] private PickaxesConfig pickaxesConfig;
        [SerializeField] private ResourcesConfig resourcesConfig;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private HotBar hotBar;
        [SerializeField] private ResourcesManager resourcesManager;
        [SerializeField] private ScreenManager screenManager;
        
        [Space]
        [SerializeField] private Button backButton;
        [SerializeField] private Button repairButton;

        [Space]
        [SerializeField] private Slider hp;
        [SerializeField] private Image brokenIcon;
        [SerializeField] private Image repairedIcon;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private Image priceIcon;

        private void Start()
        {
            backButton.Clicked.Subscribe(OnBackClicked).AddTo(this);
            repairButton.Clicked.Subscribe(OnRepairClicked).AddTo(this);
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
            var resourceType = resourcesManager.GetMainResource(hotBar.Pickaxe.Tier);

            brokenIcon.sprite = data.Sprite;
            repairedIcon.sprite = data.Sprite;
            hp.maxValue = hotBar.Pickaxe.MaxDurability;
            hp.value = hotBar.Pickaxe.Durability;
            price.text = $"{resourcesManager.Get(resourceType).Amount}/10";
            priceIcon.sprite = resourcesConfig.GetData(resourceType).Icon;
        }

        private void OnBackClicked(Unit _)
        {
            screenManager.OpenScreen(ScreenType.Inventory);
        }

        private void OnRepairClicked(Unit _)
        {
            if(resourcesManager.TryTakeResource(resourcesManager.GetMainResource(hotBar.Pickaxe.Tier), 10))
            {
                soundManager.PlaySound(SoundType.Repair);
                hotBar.Pickaxe.Repair();
                UpdateView();
            }
        }

        private void Reset()
        {
            screenManager = FindObjectOfType<ScreenManager>();
        }
    }
}
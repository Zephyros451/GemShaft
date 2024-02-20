using GemShaft.Data;
using GemShaft.Managers.Screen;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GemShaft.UI
{
    public class StoreItem : MonoBehaviour
    {
        [SerializeField] private int amount;
        [SerializeField] private int price;
        [SerializeField] private ResourceType type;
        
        [Space]
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI amountLabel;
        [SerializeField] private TextMeshProUGUI priceLabel;
        [SerializeField] private Button button;

        private ResourcesManager resourcesManager;

        public void Init(ResourcesConfig config, ResourcesManager resourcesManager)
        {
            this.resourcesManager = resourcesManager;
            
            var data = config.GetData(type);

            icon.sprite = data.Icon;
            amountLabel.text = amount.ToString();
            priceLabel.text = price.ToString();

            button.Clicked.Subscribe(OnClick).AddTo(this);
        }

        private void OnClick(Unit _)
        {
            if (resourcesManager.TryTakeResource(ResourceType.Amethyst, price))
            {
                resourcesManager.AddResource(type, amount);
            }
        }
    }
}
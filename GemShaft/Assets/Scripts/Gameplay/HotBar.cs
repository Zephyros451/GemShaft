using GemShaft.Data;
using UnityEngine;
using UnityEngine.UI;

namespace GemShaft.Gameplay
{
    public class HotBar : MonoBehaviour
    {
        [SerializeField] private PickaxesConfig pickaxesConfig;

        [SerializeField] private Image icon;
        [SerializeField] private Slider durabilitySlider;

        public Pickaxe Pickaxe { get; private set; }
        public bool IsPickaxePremium { get; private set; }

        private void Start()
        {
            var data = pickaxesConfig.GetData(Tier.Tier1);
            Pickaxe = new Pickaxe(data);
            
            UpdateView();
        }

        private void OnEnable()
        {
            if (Pickaxe == null)
            {
                return;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            var data = pickaxesConfig.GetData(Pickaxe.Tier);
            icon.sprite = data.Sprite;
            UpdateDurability();
        }

        public bool UsePickaxe()
        {
            if (Pickaxe.Durability <= 0)
            {
                return false;
            }
            
            Pickaxe.Durability--;
            UpdateDurability();
            return true;
        }

        private void UpdateDurability()
        {
            durabilitySlider.maxValue = Pickaxe.MaxDurability;
            durabilitySlider.value = Pickaxe.Durability;

            icon.sprite = Pickaxe.Durability <= 0 ? Pickaxe.Data.BrokenSprite : Pickaxe.Data.Sprite;
        }

        public void Upgrade()
        {
            var nextTier = Pickaxe.Tier == Tier.Tier6 ? Tier.Tier6 : Pickaxe.Tier + 1;
            var data = pickaxesConfig.GetData(nextTier);
            Pickaxe = new Pickaxe(data);
        }

        public void SetPremiumPickaxe()
        {
            var data = pickaxesConfig.GetData(Tier.Tier6);
            Pickaxe = new Pickaxe(data);
            IsPickaxePremium = true;
        }
    }

    public record Pickaxe
    {
        public Tier Tier;
        public float Efficiency;
        public float Durability;
        public float MaxDurability;

        public PickaxeData Data;

        public Pickaxe(PickaxeData data)
        {
            Tier = data.Tier;
            Efficiency = data.Efficiency;
            Durability = data.Durability;
            MaxDurability = data.Durability;

            Data = data;
        }

        public void Repair()
        {
            Durability = MaxDurability;
        }
    }
}
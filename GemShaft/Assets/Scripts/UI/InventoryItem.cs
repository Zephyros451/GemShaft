using GemShaft.Data;
using GemShaft.Managers.Screen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GemShaft.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI amount;
        [SerializeField] private TextMeshProUGUI label;

        public void Init(Resource resource, ResourceData data)
        {
            icon.sprite = data.Icon;
            amount.text = resource.Amount.ToString();
            label.text = data.Name;
        }
    }
}
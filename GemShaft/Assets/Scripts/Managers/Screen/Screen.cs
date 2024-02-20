using UnityEngine;

namespace GemShaft.Managers.Screen
{
    public class Screen : MonoBehaviour
    {
        [field: SerializeField] public ScreenType Type { get; private set; }
    }
}
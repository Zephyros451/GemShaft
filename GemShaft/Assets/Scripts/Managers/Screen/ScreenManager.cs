using UnityEngine;

namespace GemShaft.Managers.Screen
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private Screen[] screens;

        private void Start()
        {
            OpenScreen(ScreenType.Mine);
        }

        public void OpenScreen(ScreenType type)
        {
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(screen.Type == type);
            }
        }
    }
}
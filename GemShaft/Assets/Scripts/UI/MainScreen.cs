using GemShaft.Managers.Screen;
using UniRx;
using UnityEngine;

namespace GemShaft.UI
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] private ScreenManager screenManager;
        
        [Space]
        [SerializeField] private Button mineButton;
        [SerializeField] private Button craftButton;

        public void Start()
        {
            mineButton.Clicked.Subscribe(OnMineClicked).AddTo(this);
            craftButton.Clicked.Subscribe(OnCraftClicked).AddTo(this);
        }

        private void OnMineClicked(Unit _)
        {
            screenManager.OpenScreen(ScreenType.Mine);
        }

        private void OnCraftClicked(Unit _)
        {
            screenManager.OpenScreen(ScreenType.Craft);
        }
    }
}
using Coffee.UIExtensions;
using GemShaft.Data;
using GemShaft.Managers;
using GemShaft.Managers.Screen;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Button = GemShaft.UI.Button;

namespace GemShaft.Gameplay
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private float cellSize;

        [Space]
        [SerializeField] private Button button;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI hp;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private UIParticle digParticles;
        [SerializeField] private UIParticle destroyParticles;

        public readonly Subject<Vector2Int> PremiumClicked = new();
        public readonly Subject<Vector2Int> Destroyed = new();

        private readonly CompositeDisposable disp = new();

        private ResourcesManager resourcesManager;
        private SoundManager soundManager;
        private HotBar hotBar;
        private Board board;

        private CellType type;
        private float durability;
        private Vector2Int position;
        private bool isHolding;
        private float holdingCooldown;

        private const int Space = 2;
        
        public RectTransform RectTransform => rectTransform;

        public void Inject(ResourcesManager resourcesManager, SoundManager soundManager, HotBar hotBar, Board board)
        {
            this.resourcesManager = resourcesManager;
            this.soundManager = soundManager;
            this.hotBar = hotBar;
            this.board = board;
        }

        public void Init(CellData data)
        {
            type = data.Type;
            icon.sprite = data.Icon;
            durability = data.Durability;

            button.Pressed.Subscribe(OnPressed).AddTo(disp);
            button.Released.Subscribe(OnReleased).AddTo(disp);
            button.Clicked.Subscribe(OnClicked).AddTo(disp);
            button.Entered.Subscribe(OnEntered).AddTo(disp);
            button.Exited.Subscribe(OnExited).AddTo(disp);
            
            UpdateView();
        }

        public void SetIndex(int column, int row)
        {
            position = new Vector2Int(column, row);
        }

        public void SetPosition(int columCount, int column, int row)
        {
            rectTransform.anchoredPosition =
                new Vector2(CalculateCellColumnPosition(columCount, column), CalculateCellRowPosition(row));
        }

        public float CalculateCellColumnPosition(int columnCount, int column)
        {
            return (column -  columnCount / 2) * (cellSize + Space);
        }

        public float CalculateCellRowPosition(int position)
        {
            return -position * (cellSize + Space);
        }

        private void OnPressed(Unit _)
        {
            isHolding = true;
        }

        private void OnReleased(Unit _)
        {
            isHolding = false;
        }

        private void OnEntered(Unit _)
        {
            isHolding = true;
        }

        private void OnExited(Unit _)
        {
            isHolding = false;
        }

        public void ManualClicked()
        {
            durability -= hotBar.Pickaxe.Efficiency;

            if (durability <= 0)
            {
                digParticles.Clear();
                var particles = Instantiate(destroyParticles, board.transform);
                particles.transform.position = destroyParticles.transform.position;
                particles.Play();
                isHolding = false;
                resourcesManager.GainResources(type);
                soundManager.PlaySound(SoundType.BlockDestroyed);
                disp.Clear();
                Destroyed.OnNext(position);
                return;
            }

            soundManager.PlaySound(SoundType.BlockDigging);
            digParticles.Play();

            UpdateView();
        }

        private void OnClicked(Unit _)
        {
            if (hotBar.IsPickaxePremium)
            {
                PremiumClicked.OnNext(position);
                return;
            }
            
            if (!hotBar.UsePickaxe())
            {
                soundManager.PlaySound(SoundType.BrokenPickaxe);
                return;
            }
            
            durability -= hotBar.Pickaxe.Efficiency;

            if (durability <= 0)
            {
                digParticles.Clear();
                var particles = Instantiate(destroyParticles, board.transform);
                particles.transform.position = destroyParticles.transform.position;
                particles.Play();
                isHolding = false;
                resourcesManager.GainResources(type);
                soundManager.PlaySound(SoundType.BlockDestroyed);
                disp.Clear();
                Destroyed.OnNext(position);
                return;
            }

            soundManager.PlaySound(SoundType.BlockDigging);
            digParticles.Play();

            UpdateView();
        }

        private void Update()
        {
            if (!isHolding || !Input.GetMouseButton(0))
            {
                holdingCooldown = 0f;
                return;
            }

            if (holdingCooldown > 0.2f)
            {
                holdingCooldown = 0f;
                OnClicked(default);
            }
            else
            {
                holdingCooldown += Time.deltaTime;
            }
        }

        private void UpdateView()
        {
            hp.text = ((int)durability).ToString();
        }
    }
}
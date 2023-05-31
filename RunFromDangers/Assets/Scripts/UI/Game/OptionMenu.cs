using Gameplay.Levels;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class OptionMenu : MonoBehaviour
    {
        [SerializeField]
        private LevelController _levelController;
        [SerializeField] 
        private GameObject _mainMenyGameObject;

        [SerializeField]
        private ValueSlider _enemyMaxCountSlider;
        [SerializeField]
        private ValueSlider _enemySpawnRateSlider;
        [SerializeField]
        private ValueSlider _bombsMaxCountSlider;
        [SerializeField]
        private ValueSlider _bombsSpawnRateSlider;
        [SerializeField]
        private Button _backButton;

        private void Awake()
        {
            _backButton.onClick.AddListener(() =>
            {
                _mainMenyGameObject.SetActive(true);
                gameObject.SetActive(false);
            });
        }

        private void OnEnable()
        {
            Initialize();
            AddListeners();
        }

        private void Initialize()
        {
            _enemyMaxCountSlider.Initialize(_levelController.EnemyMaxCount);
            _enemySpawnRateSlider.Initialize(_levelController.EnemySpawnRate);
            _bombsMaxCountSlider.Initialize(_levelController.BombsMaxCount);
            _bombsSpawnRateSlider.Initialize(_levelController.BombsSpawnRate);
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            _enemyMaxCountSlider.OnValueChanged += UpdateEnemyMaxCount;
            _enemySpawnRateSlider.OnValueChanged += UpdateEnemySpawnRate;
            _bombsMaxCountSlider.OnValueChanged += UpdateBombsMaxCount;
            _bombsSpawnRateSlider.OnValueChanged += UpdateBombsSpawnRate;
        }

        private void RemoveListeners()
        {
            _enemyMaxCountSlider.OnValueChanged -= UpdateEnemyMaxCount;
            _enemySpawnRateSlider.OnValueChanged -= UpdateEnemySpawnRate;
            _bombsMaxCountSlider.OnValueChanged -= UpdateBombsMaxCount;
            _bombsSpawnRateSlider.OnValueChanged -= UpdateBombsSpawnRate;
        }

        private void UpdateEnemyMaxCount(int value)
        {
            _levelController.EnemyMaxCount = value;
        }

        private void UpdateEnemySpawnRate(int value)
        {
            _levelController.EnemySpawnRate = value;
        }

        private void UpdateBombsMaxCount(int value)
        {
            _levelController.BombsMaxCount = value;
        }

        private void UpdateBombsSpawnRate(int value)
        {
            _levelController.BombsSpawnRate = value;
        }
    }
}
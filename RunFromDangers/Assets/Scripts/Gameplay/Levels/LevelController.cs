using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameCore;
using Gameplay.Characters.Bombs;
using Gameplay.Characters.Enemy;
using Gameplay.Characters.Main;
using UI.Elements;
using UnityEngine;

namespace Gameplay.Levels
{
    public class LevelController : MonoBehaviour
    {
        public int EnemyMaxCount { get; set; } = 50;
        public int EnemySpawnRate { get; set; } = 1;
        public int BombsMaxCount { get; set; } = 20;
        public int BombsSpawnRate { get; set; } = 2;

        public event Action OnGameEnded;

        public float GameTimeSec => _gameTimeSec;

        [SerializeField]
        private MainCharacterSpawner _mainCharacterSpawner;
        [SerializeField]
        private EnemiesSpawner _enemiesSpawner;
        [SerializeField]
        private BombsSpawner _bombsSpawner;

        private MainCharacter _mainCharacter;
        private float _gameTimeSec;
        private bool _isGameGone;

        private List<EnemyCharacter> _enemies = new List<EnemyCharacter>();
        private List<BombCharacter> _bombs = new List<BombCharacter>();

        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            Core.GameUpdate.UpdateAction += CustomUpdate;
        }

        private void CustomUpdate(float deltaTime)
        {
            if (_isGameGone)
            {
                _gameTimeSec += deltaTime;
            }
        }

        public void StartGame()
        {
            _isGameGone = true;
            _gameTimeSec = 0;

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            CreateMainCharacter();
            CreateEnemies(cancellationToken);
            CreateBombs(cancellationToken);
        }

        private void OnDestroy()
        {
            Core.GameUpdate.UpdateAction -= CustomUpdate;
            _cancellationTokenSource?.Cancel();
        }

        private void CreateMainCharacter()
        {
            _mainCharacter = _mainCharacterSpawner.CreateCharacter();
            _mainCharacter.OnDestroyAction += StopGame;
        }

        private async Task CreateEnemies(CancellationToken cancellationToken)
        {
            for (int i = 0; i < EnemyMaxCount; i++)
            {
                await Task.Delay(EnemySpawnRate * 1000, cancellationToken);

                EnemyCharacter enemyCharacter = _enemiesSpawner.CreateEnemyCharacters(_mainCharacter.transform);
                enemyCharacter.OnDestroyAction += RemoveEnemy;
                _enemies.Add(enemyCharacter);
            }
        }

        private async Task CreateBombs(CancellationToken cancellationToken)
        {
            for (int i = 0; i < BombsMaxCount; i++)
            {
                await Task.Delay(BombsSpawnRate * 1000, cancellationToken);

                BombCharacter bombCharacter = _bombsSpawner.CreateBombsCharacters();
                bombCharacter.OnDestroyAction += RemoveBomb;
                _bombs.Add(bombCharacter);
            }
        }

        private void RemoveEnemy(EnemyCharacter enemyCharacter)
        {
            _enemies.Remove(enemyCharacter);
        }

        private void RemoveBomb(BombCharacter bomb)
        {
            _bombs.Remove(bomb);
        }

        private void StopGame()
        {
            OnGameEnded?.Invoke();

            _isGameGone = false;
            _cancellationTokenSource.Cancel();

            for (int i = 0; i < _bombs.Count; i++)
            {
                Destroy(_bombs[i].gameObject);
            }

            for (int i = 0; i < _enemies.Count; i++)
            {
                Destroy(_enemies[i].gameObject);
            }

            _bombs.Clear();
            _enemies.Clear();
        }
    }
}
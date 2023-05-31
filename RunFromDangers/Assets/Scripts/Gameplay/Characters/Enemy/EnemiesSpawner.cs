using System;
using GameCore;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Gameplay.Characters.Enemy
{
    [Serializable]
    public class EnemiesSpawner
    {
        [SerializeField]
        private EnemyCharacter _enemyCharacterPrefab;

        [SerializeField]
        private Vector2 _spawnPositionX;
        [SerializeField]
        private Vector2 _spawnPositionZ;
        [SerializeField]
        private float _spawnPositionY;

        public EnemyCharacter CreateEnemyCharacters(Transform target)
        {
            EnemyCharacter enemyCharacter = Object.Instantiate(_enemyCharacterPrefab, GetSpawnPosition(), Quaternion.identity);
            enemyCharacter.Initialize(Core.GameUpdate, target.transform);

            return enemyCharacter;
        }

        private Vector3 GetSpawnPosition()
        {
            float x = Random.Range(_spawnPositionX.x, _spawnPositionX.y);
            float z = Random.Range(_spawnPositionZ.x, _spawnPositionZ.y);

            return new Vector3(x, _spawnPositionY, z);
        }
    }
}
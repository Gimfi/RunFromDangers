using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Gameplay.Characters.Bombs
{
    [Serializable]
    public class BombsSpawner
    {
        [SerializeField]
        private BombCharacter _enemyCharacterPrefab;

        [SerializeField]
        private Vector2 _spawnPositionX;
        [SerializeField]
        private Vector2 _spawnPositionZ;
        [SerializeField]
        private float _spawnPositionY;

        public BombCharacter CreateBombsCharacters()
        {
            BombCharacter bombCharacter = Object.Instantiate(_enemyCharacterPrefab, GetSpawnPosition(), Quaternion.identity);
            //enemyCharacter.Initialize(Core.GameUpdate, target.transform);

            return bombCharacter;
        }

        private Vector3 GetSpawnPosition()
        {
            float x = Random.Range(_spawnPositionX.x, _spawnPositionX.y);
            float z = Random.Range(_spawnPositionZ.x, _spawnPositionZ.y);

            return new Vector3(x, _spawnPositionY, z);
        }
    }
}
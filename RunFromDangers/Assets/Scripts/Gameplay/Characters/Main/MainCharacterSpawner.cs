using System;
using GameCore;
using Gameplay.Camera;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Characters.Main
{
    [Serializable]
    public class MainCharacterSpawner
    {
        [SerializeField]
        private CharacterCamera _characterCamera;
        [SerializeField]
        private MainCharacter _mainCharacterPrefab;
        [SerializeField]
        private Vector3 _startPosition;

        public MainCharacter CreateCharacter()
        {
            MainCharacter mainCharacter = Object.Instantiate(_mainCharacterPrefab, _startPosition, Quaternion.identity);
            mainCharacter.Initialize(Core.GameUpdate, Core.InputController);

            CreateCamera(mainCharacter);

            return mainCharacter;
        }

        private void CreateCamera(MainCharacter mainCharacter)
        {
            _characterCamera.Initialize(Core.GameUpdate, mainCharacter);
        }
    }
}
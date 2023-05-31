using GameCore.Update;
using Gameplay.Characters.Main;
using UnityEngine;

namespace Gameplay.Camera
{
    public class CharacterCamera : MonoBehaviour
    {
        [SerializeField]
        private float _followDistance = 30.0f;
        [SerializeField]
        private float _elevationAngle = 30.0f;
        [SerializeField]
        private float _movementSmoothingValue = 25f;
        [SerializeField]
        private float _rotationSmoothingValue = 5.0f;

        private GameUpdate _gameUpdate;

        private MainCharacter _cameraTarget;
        private Transform _cameraTransform;
        private Vector3 _currentVelocity = Vector3.zero;
        private Vector3 _desiredPosition;

        private void Awake()
        {
            _cameraTransform = transform;
        }

        public void Initialize(GameUpdate gameUpdate, MainCharacter target)
        {
            _gameUpdate = gameUpdate;
            _cameraTarget = target;

            AddListeners();
        }

        private void AddListeners()
        {
            _gameUpdate.UpdateAction += CustomUpdate;
            _cameraTarget.OnDestroyAction += OnCharacterDestroy;
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            if (_gameUpdate != null)
            {
                _gameUpdate.UpdateAction -= CustomUpdate;
            }

            if (_cameraTarget != null)
            {
                _cameraTarget.OnDestroyAction -= OnCharacterDestroy;
            }
        }

        private void OnCharacterDestroy()
        {
            _cameraTarget.OnDestroyAction -= OnCharacterDestroy;
            _cameraTarget = null;
        }

        private void CustomUpdate(float deltaTime)
        {
            if (_cameraTarget != null)
            {
                _desiredPosition = _cameraTarget.transform.position + _cameraTarget.transform.TransformDirection(Quaternion.Euler(_elevationAngle, 0, 0f) * (new Vector3(0, 0, -_followDistance)));
                _cameraTransform.position = Vector3.SmoothDamp(_cameraTransform.position, _desiredPosition, ref _currentVelocity, _movementSmoothingValue * deltaTime);
                _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, Quaternion.LookRotation(_cameraTarget.transform.position - _cameraTransform.position), _rotationSmoothingValue * deltaTime);
            }
        }
    }
}
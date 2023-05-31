using GameCore.Input;
using GameCore.Update;
using Gameplay.Characters.Bombs;
using Gameplay.Characters.Enemy;
using System;
using UnityEngine;

namespace Gameplay.Characters.Main
{
    public class MainCharacter : MonoBehaviour
    {
        public event Action OnDestroyAction;

        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private GameObject _explosionVFX;

        private GameUpdate _gameUpdate;
        private InputController _inputController;

        private Vector3 moveDirection;
        private float _gravity = -9.81f;
        private float _velocity;

        public void Initialize(GameUpdate gameUpdate, InputController inputController)
        {
            _gameUpdate = gameUpdate;
            _inputController = inputController;

            AddListeners();
        }

        private void AddListeners()
        {
            _gameUpdate.UpdateAction += CustomUpdate;
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            _gameUpdate.UpdateAction -= CustomUpdate;
        }

        private void CustomUpdate(float deltaTime)
        {
            ApplyMoveDirection(deltaTime);
            ApplyGravity(deltaTime);

            _controller.transform.Rotate(new Vector3(0, _inputController.Horizontal * deltaTime * _rotationSpeed, 0));
            _controller.Move(moveDirection * deltaTime * _speed);
        }

        private void ApplyMoveDirection(float deltaTime)
        {
            moveDirection = _controller.transform.forward * _inputController.Vertical;
        }

        private void ApplyGravity(float deltaTime)
        {
            if (_controller.isGrounded && _velocity < 0)
            {
                _velocity = -1;
            }
            else
            {
                _velocity += _gravity * deltaTime;
            }

            moveDirection.y = _velocity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BombCharacter bombCharacter))
            {
                Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                bombCharacter.Destroy();
                OnDestroyAction?.Invoke();
                Destroy(gameObject);
            }

            if (other.gameObject.TryGetComponent(out EnemyCharacter enemyCharacter))
            {
                Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                enemyCharacter.Destroy();
                OnDestroyAction?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
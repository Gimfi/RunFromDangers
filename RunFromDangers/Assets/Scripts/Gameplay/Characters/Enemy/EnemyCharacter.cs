using System;
using GameCore.Update;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Characters.Enemy
{
    public class EnemyCharacter : MonoBehaviour
    {
        public event Action<EnemyCharacter> OnDestroyAction;

        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField] 
        private GameObject _explosionVFX;

        private GameUpdate _gameUpdate;
        private Transform _target;

        public void Initialize(GameUpdate gameUpdate, Transform target)
        {
            _gameUpdate = gameUpdate;
            _target = target;

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
            ApplyMove(deltaTime);
        }

        private void ApplyMove(float deltaTime)
        {
            _agent.SetDestination(_target.position);
        }

        public void Destroy()
        {
            OnDestroyAction?.Invoke(this);

            Instantiate(_explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
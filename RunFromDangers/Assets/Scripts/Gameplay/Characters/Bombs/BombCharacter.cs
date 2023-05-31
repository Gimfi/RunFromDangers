using System;

namespace Gameplay.Characters.Bombs
{
    using UnityEngine;

    public class BombCharacter : MonoBehaviour
    {
        public event Action<BombCharacter> OnDestroyAction;

        [SerializeField] private GameObject _explosionVFX;
        
        public void Destroy()
        {
            OnDestroyAction?.Invoke(this);

            Instantiate(_explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
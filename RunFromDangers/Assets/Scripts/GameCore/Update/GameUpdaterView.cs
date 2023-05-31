using System;
using UnityEngine;

namespace GameCore.Update
{
    public class GameUpdaterView : MonoBehaviour
    {
        public event Action<float> UpdateAaction;

        private void Update()
        {
            UpdateAaction?.Invoke(Time.deltaTime);
        }
    }
}